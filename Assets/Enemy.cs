using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Animator camAnim;
    public int health;
    public Animator deathAnim;
    public GameObject explosion;

    private void Update()
    {
        if (health <= 0)
        {
            Instantiate(deathAnim, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        camAnim.SetTrigger("shake");
        Instantiate(explosion, transform.position, Quaternion.identity);
        health -= damage;
    }
}