using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public float offset;
    public GameObject projectile;
    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public float eastOffsetDistance = 0.5f;
    public int bulletCount = 3;
    [SerializeField] private Text bulletCountText;

    private void Start()
    {
        UpdateBulletCountUI();
    }

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (difference.x < 0)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (timeBtwShots <= 0 && bulletCount > 0)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 eastOffset = shotPoint.position + (transform.right * eastOffsetDistance);

                Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - eastOffset);
                direction.z = 0;
                direction.Normalize();

                GameObject newProjectile = Instantiate(projectile, eastOffset, Quaternion.identity);
                newProjectile.GetComponent<Projectile>().SetDirection(direction);
                timeBtwShots = startTimeBtwShots;
                bulletCount--;
                UpdateBulletCountUI();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void AddBullets(int amount)
    {
        bulletCount += amount;
        bulletCount = Mathf.Clamp(bulletCount, 0, 3);
        UpdateBulletCountUI();
    }

    private void UpdateBulletCountUI()
    {
        bulletCountText.text = "Bullets: " + bulletCount;
    }
}