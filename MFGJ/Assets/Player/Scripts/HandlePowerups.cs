using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class HandlePowerups : MonoBehaviour
{
    public GameObject[] oilSpillPrefabs = new GameObject[4];
    public GameObject exhaustPoint;

    PlayerMovement movement;
    PlayerHealth health;
    GameObject powerup;

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
        powerup = col.gameObject;
        string colTag = col.gameObject.tag;

        if (colTag == "SpeedBoost") StartCoroutine(ChangeSpeed(1.5f));
        else if (colTag == "Debuff") StartCoroutine(ChangeSpeed(0.5f));
        else if (colTag == "Upsize") StartCoroutine(ChangeSize(2f));
        else if (colTag == "Downsize") StartCoroutine(ChangeSize(0.5f));
        else if (colTag == "OilSpill") StartCoroutine(SpillOil());
        else if (colTag == "Invincibility") StartCoroutine(MakeInvincible());
        else if (colTag == "Boom") ExplodeNearestRacer();
        else if (colTag == "Rehydrate") Rehydrate();
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
        Destroy(powerup);
    }

    void Rehydrate()
    {
        health.hydration += 15;

        if (health.hydration > 50) health.hydration = 50;
        Destroy(powerup);
    }

    void SpawnOilSpill()
    {
        int i = UnityEngine.Random.Range(0, 3);

        Instantiate(oilSpillPrefabs[i], exhaustPoint.transform.position, Quaternion.identity);
    }

    IEnumerator SpillOil()
    {
        Destroy(powerup);
        InvokeRepeating("SpawnOilSpill", 0f, 0.2f);
        yield return new WaitForSeconds(5f);
        CancelInvoke();
    }

    IEnumerator ChangeSize(float resizeFactor) // TODO: Create size up animation
    {
        Destroy(powerup);
        transform.localScale *= resizeFactor;
        yield return new WaitForSeconds(5f);
        transform.localScale /= resizeFactor;
    }

    IEnumerator ChangeSpeed(float speedFactor) // TODO: Add tailwind effect
    {
        Destroy(powerup);
        movement.speedBiasX *= speedFactor;
        yield return new WaitForSeconds(3f);
        movement.speedBiasX /= speedFactor;
    }

    IEnumerator MakeInvincible()
    {
        Destroy(powerup);
        health.damageable = false;
        yield return new WaitForSeconds(10f);
        health.damageable = true;
    }
}
