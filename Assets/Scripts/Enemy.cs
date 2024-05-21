using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator camAnim;
    public int health;
    public GameObject deathAnimPrefab; // Changed to GameObject
    public GameObject explosion;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private enum MovementState { idle, running }

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (health <= 0)
        {
            Instantiate(deathAnimPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        UpdateAnimationState();
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

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
    }
}
