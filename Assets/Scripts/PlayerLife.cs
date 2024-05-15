using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
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
            Destroy(DeathController.BoxCollider2D);
            Die();
        }
    }

    private void Die()
    {
        Destroy(GetComponent<PlayerMovement>());
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
        }
    }
}