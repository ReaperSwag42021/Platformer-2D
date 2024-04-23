using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSoundEffect;
    [SerializeField] private AudioClip landingSoundEffect;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private float stepInterval = 0.5f;
    private float stepTimer;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // Start playing background music
        audioSource.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            PlaySoundEffect(jumpSoundEffect);
        }

        UpdateAnimationState();
        PlayFootsteps();
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

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void PlayFootsteps()
    {
        // Increment the step timer
        stepTimer += Time.deltaTime;

        // Check if the player has taken a step
        if (stepTimer >= stepInterval && IsGrounded() && (dirX != 0))
        {
            PlaySoundEffect(footstepSound);
            // Reset the step timer
            stepTimer = 0f;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                // Check if the collision normal is approximately up (i.e., we've hit a horizontal surface)
                if (Vector2.Dot(point.normal, Vector2.up) > 0.9f)
                {
                    PlaySoundEffect(landingSoundEffect);
                }
            }
        }
    }
}