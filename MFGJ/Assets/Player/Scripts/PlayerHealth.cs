using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    GameObject explosionParticles, bloodParticles, smokeParticles, deathScreen;
    [SerializeField]
    private int dehydrationRate = 1;

    public Slider hydrationMeter;
    public Slider healthBar;
    public int hydration = 50;
    public bool damageable = true;

    [SerializeField]
    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = 100;
        hydrationMeter.maxValue = 50;
        InvokeRepeating("Dehydrate", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        hydrationMeter.value = hydration;
        healthBar.value = health;

        if (health <= 0 || hydration <= 0) Die();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Icicle")
        {
            TakeDamage(10);
            Destroy(col.gameObject);
        }
    }

    void Dehydrate()
    {
        hydration -= dehydrationRate;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Meteor")
        {
            TakeDamage(25);
            Instantiate(explosionParticles, col.gameObject.transform.position, Quaternion.identity);
            Destroy(col.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Lava") health -= 2;
    }

    public void TakeDamage(int damage)
    {
        if (damageable) health -= damage;
    }

    public void Die()
    {
        Instantiate(smokeParticles, transform.position, Quaternion.identity);
        Instantiate(bloodParticles, transform.position, Quaternion.identity);
        deathScreen.SetActive(true);
        Destroy(gameObject);
    }
}
