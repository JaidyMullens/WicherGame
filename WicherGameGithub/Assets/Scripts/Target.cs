using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public GameObject deathEffect;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject cubeDeath = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
