using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionParticles, bloodParticles, smokeParticles;

    [SerializeField]
    private int health = 200;

    void Update()
    {
        if (health <= 0) Die();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Meteor") 
        {
            health -= 25;
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Lava") health--;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Icicle")
        {
            health -= 10;
            Destroy(col.gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    void Die()
    {
        Instantiate(bloodParticles, transform.position, Quaternion.identity);
        Instantiate(smokeParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
