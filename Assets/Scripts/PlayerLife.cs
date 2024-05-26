using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public GameMaster gameMaster;
    public int health = 3;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;

    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
public void Die()
    {
        Destroy(GetComponent<PlayerMovement>());
        Destroy(transform.Find("Weapon")?.gameObject);
        rb.velocityX = 0;
        deathSoundEffect.Play();
        anim.SetTrigger("death");
        StartCoroutine("Restartlevel");
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
}