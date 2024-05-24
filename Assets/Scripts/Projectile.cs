using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
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

        // Calculate the angle in degrees and rotate the projectile to face that direction
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        // Move the projectile in the direction it's facing
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the Enemy script from the collided game object
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // Call the TakeDamage method on the Enemy script
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
