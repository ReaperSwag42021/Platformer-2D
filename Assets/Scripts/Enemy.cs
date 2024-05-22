using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator camAnim;
    public int health;
    public GameObject deathAnimPrefab;  // This should be a visual effect, not an enemy
    public GameObject explosion;
    public float moveSpeed = 5f;
    public float chaseDistance = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private Transform player;
    private bool isFacingRight = true;
    private bool isDead = false;
    private enum MovementState { idle, running }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMovement();
        }

        UpdateAnimationState();
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void StopMovement()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        camAnim.SetTrigger("shake");
        Instantiate(explosion, transform.position, Quaternion.identity);
        health -= damage;

        Debug.Log("Enemy took damage. Current health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        rb.velocity = Vector2.zero; // Stop movement

        Debug.Log("Enemy is dying.");

        if (deathAnimPrefab != null)
        {
            Debug.Log("Instantiating death animation prefab.");
            Instantiate(deathAnimPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("deathAnimPrefab is not assigned.");
        }

        // Disable the enemy's collider and renderer to avoid further interaction and visibility
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) renderer.enabled = false;

        // Optionally, stop the animator if you want to stop all animations
        if (anim != null)
        {
            anim.SetTrigger("die");
        }

        // Destroy the game object after a delay to ensure all effects are played
        Destroy(gameObject, 0.5f);
    }

    private void UpdateAnimationState()
    {
        if (isDead) return;

        MovementState state;

        if (rb.velocity.x != 0)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
