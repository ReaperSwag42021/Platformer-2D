using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public GameMaster gameMaster;
    public int maxHealth = 3;
    public int currentHealth;
    public HealthBar healthBar;
    public Text invincibilityText;
    bool isDying = false;
    private bool isInvincible = false;
    public Finish finish;

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
        invincibilityText.text = "Invincible: ";
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
        if (!isInvincible)
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
            rb.velocity = Vector2.zero;
            deathSoundEffect.Play();
            anim.SetTrigger("death");
            StartCoroutine(Restartlevel());
        }
    }

    public IEnumerator Restartlevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StopAllCoroutines();
        gameMaster.RestartLevel();
    }

    public void BecomeInvincible()
    {
        isInvincible = true;
        invincibilityText.gameObject.SetActive(true);
        StartCoroutine(InvincibilityCountdown(6));

        GetComponent<SpriteRenderer>().color = Color.yellow;
        Transform weaponTransform = transform.Find("Weapon");
        if (weaponTransform != null)
        {
            weaponTransform.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    private IEnumerator InvincibilityCountdown(int duration)
    {
        while (duration > 0)
        {
            invincibilityText.text = "Invincible: " + duration;
            yield return new WaitForSeconds(1);
            duration--;
        }

        invincibilityText.text = "Invincible: ";
        StopInvincibility();
    }

    private void StopInvincibility()
    {
        isInvincible = false;
        invincibilityText.gameObject.SetActive(false);

        GetComponent<SpriteRenderer>().color = Color.white;
        Transform weaponTransform = transform.Find("Weapon");
        if (weaponTransform != null)
        {
            weaponTransform.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void StopAllCoroutinesInPlayerLife()
    {
        StopAllCoroutines();
    }
}