using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    public LayerMask whatIsSolid;

    private Vector2 moveDirection;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Ground")
        {
            DestroyProjectile();
            Debug.Log("Projectile hit the ground");
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Debug.Log("Projectile hit enemy!");

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}