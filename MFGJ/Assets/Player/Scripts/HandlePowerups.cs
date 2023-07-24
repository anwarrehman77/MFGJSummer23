using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class HandlePowerups : MonoBehaviour
{
    PlayerMovement movement;
    PlayerHealth health;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        string colTag = col.gameObject.tag;

        if (colTag == "SpeedBoost") StartCoroutine(ChangeSpeed(1.5f));
        else if (colTag == "Debuff") StartCoroutine(ChangeSpeed(0.5f));
        else if (colTag == "Upsize") StartCoroutine(ChangeSize(2f));
        else if (colTag == "Downsize") StartCoroutine(ChangeSize(0.5f));
        else if (colTag == "OilSpill") StartCoroutine(SpillOil());
        else if (colTag == "Invincibility") StartCoroutine(MakeInvincible());
        else if (colTag == "Boom") ExplodeNearestRacer();
        else if (colTag == "Rehydrate") Rehydrate();

        Destroy(col.gameObject);
    }

    void ExplodeNearestRacer()
    {
        float minDistance = float.MaxValue;
        GameObject nearestRacer = null;

        foreach (GameObject racer in GameObject.FindGameObjectsWithTag("Racer"))
        {
            float distance = Vector2.Distance(transform.position, racer.transform.position);
            if (minDistance > distance)
            {
                minDistance = distance;
                nearestRacer = racer;
            }
        }

        if (nearestRacer != null) Destroy(nearestRacer);
    }

    void Rehydrate()
    {
        health.hydration += 15;

        if (health.hydration > 50) health.hydration = 50;
    }

    IEnumerator SpillOil()
    {
        yield return new WaitForSeconds(10f);
    }

    IEnumerator ChangeSize(float resizeFactor) // TODO: Create size up animation
    {
        transform.localScale *= resizeFactor;
        yield return new WaitForSeconds(5f);
        transform.localScale /= resizeFactor;
    }

    IEnumerator ChangeSpeed(float speedFactor) // TODO: Add tailwind effect
    {
        movement.speedBiasX *= speedFactor;
        yield return new WaitForSeconds(3f);
        movement.speedBiasX /= speedFactor;
    }

    IEnumerator MakeInvincible()
    {
        health.damageable = false;
        yield return new WaitForSeconds(10f);
        health.damageable = true;
    }
}
