using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    public GameObject projectile;
    public GameObject shotEffect;
    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public float eastOffsetDistance = 0.5f; // Distance to offset the projectile to the east

    private void Update()
    {
        // Handles the weapon rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                // Calculate the east offset position
                Vector3 eastOffset = shotPoint.position + (transform.right * eastOffsetDistance);

                Instantiate(shotEffect, eastOffset, Quaternion.identity);

                // Calculate the direction
                Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - eastOffset);
                direction.z = 0;
                direction.Normalize();

                GameObject newProjectile = Instantiate(projectile, eastOffset, Quaternion.identity);
                newProjectile.GetComponent<Projectile>().SetDirection(direction); // Set the direction based on the normalized vector
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}