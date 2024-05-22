using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator camAnim;
    public int health;
    public GameObject deathAnimPrefab;
    public GameObject explosion;
    public float moveSpeed = 5f;
    public float chaseDistance = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private Transform player;
    private bool isFacingRight = true;
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
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMovement();
        }

        if (health <= 0)
        {
            Instantiate(deathAnimPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
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
        camAnim.SetTrigger("shake");
        Instantiate(explosion, transform.position, Quaternion.identity);
        health -= damage;
    }

    private void UpdateAnimationState()
    {
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
