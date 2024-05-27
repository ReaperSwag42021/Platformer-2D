using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public GameMaster gameMaster;
    public int maxHealth = 3;
    public int currentHealth;
    public HealthBar healthBar;
    bool isDying = false;
    private bool isInvincible = false; // Added

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;

    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(3);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible) // Added
        {
            currentHealth -= damage;
            if (healthBar != null)
            {
                healthBar.SetHealth(currentHealth);
            }
            else
            {
                Debug.LogError("HealthBar is not assigned");
            }
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if (!isDying)
        {
            isDying = true;
            Destroy(GetComponent<PlayerMovement>());
            Destroy(transform.Find("Weapon")?.gameObject);
            rb.velocityX = 0;
            deathSoundEffect.Play();
            anim.SetTrigger("death");
            StartCoroutine("Restartlevel");
        }
    }

    public IEnumerator Restartlevel()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StopAllCoroutines();
            Invoke("gameMaster.RestartLevel()", 2f);
        }
    }

    public void BecomeInvincible() // Added
    {
        isInvincible = true;
        StartCoroutine(StopInvincibility());
    }

    private IEnumerator StopInvincibility() // Added
    {
        yield return new WaitForSeconds(5);
        isInvincible = false;
    }
}
