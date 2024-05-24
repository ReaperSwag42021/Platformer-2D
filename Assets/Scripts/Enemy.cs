using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int health = 3;
    public Animator deathAnim;
    public GameObject explosion;
    public float moveSpeed = 5f;
    public float chaseDistance = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;
    private bool isFacingRight = true;
    private bool isDead = false;
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

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        Instantiate(explosion, transform.position, Quaternion.identity);
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
        rb.velocity = Vector2.zero; // Stop movement

        Debug.Log("Enemy is dying.");

        // Trigger the death animation
        if (anim != null)
        {
            anim.SetTrigger("die");
        }

        // Disable this script to stop further updates
        this.enabled = false;

        Object.Destroy(gameObject, 3f);
        Object.Destroy(rb);
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