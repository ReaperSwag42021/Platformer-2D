using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public Animator deathAnim;
    public float moveSpeed = 5f;
    public float chaseDistance = 10f;
    public float damageCooldown = 3f;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;
    private bool isFacingRight = true;
    private bool isDead = false;
    private float lastDamageTime = 0;
    private enum MovementState { idle, running }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= lastDamageTime + damageCooldown)
        {
            collision.gameObject.GetComponent<PlayerLife>().TakeDamage(1);
            lastDamageTime = Time.time;
        }
    }


    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        Debug.Log("Enemy took damage. Current health: " + health);

        if (health <= 0)
        {
            Die();
        }
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

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        rb.velocity = Vector2.zero;

        Debug.Log("Enemy is dying.");


        GetComponent<BoxCollider2D>().enabled = false;


        if (anim != null)
        {
            anim.SetTrigger("die");
        }


        this.enabled = false;

        Object.Destroy(rb);
        Object.Destroy(gameObject, 3f);
    }

    private void UpdateAnimationState()
    {
        if (isDead) return;

        MovementState state = rb.velocity.x != 0 ? MovementState.running : MovementState.idle;
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