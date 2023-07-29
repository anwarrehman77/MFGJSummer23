using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class HandlePowerups : MonoBehaviour
{
    public GameObject[] oilSpillPrefabs = new GameObject[4];
    public GameObject exhaustPoint;

    private PlayerMovement movement;
    private PlayerHealth health;
    private GameObject powerup;
    private GameObject colObject;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
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
        else if (colTag == "Flower") StartCoroutine(StunRacers());
        else if (colTag == "EggBasket") 
        {
            int powerups =UnityEngine.Random.Range(0, 8);

            switch (powerups)
            {
                case 0:
                StartCoroutine(ChangeSpeed(1.5f));
                break;
                case 1: 
                StartCoroutine(ChangeSpeed(0.5f));
                break;
                case 2:
                StartCoroutine(ChangeSize(2f));
                break;
                case 3:
                StartCoroutine(ChangeSize(0.5f));
                break;
                case 4:
                StartCoroutine(SpillOil());
                break;
                case 5:
                StartCoroutine(MakeInvincible());
                break;
                case 6:
                ExplodeNearestRacer();
                break;
                case 7:
                Rehydrate();
                break;
                case 8:
                StartCoroutine(StunRacers());
                break;
            }
        }
        else if (colTag == "Bee")
        {
            StartCoroutine(ChangeSpeed(2.5f));
        }
        else if (colTag == "Candy")
        {
            health.hydration /= 4;
            StartCoroutine(ChangeSpeed(5f));
        }
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

    IEnumerator StunRacers()
    {
        GameObject[] racers = GameObject.FindGameObjectsWithTag("Racer");
        List<GameObject> racersList = racers.ToList();
        List<GameObject> nearestRacers = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            float minDistance = float.MaxValue;
            GameObject nearestRacer = null;

            foreach (GameObject racer in racersList)
            {
                float distance = Vector2.Distance(transform.position, racer.transform.position);
                
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestRacer = racer;
                }
            }

            if (nearestRacer != null) 
            {
                nearestRacers.Add(nearestRacer);
                racersList.Remove(nearestRacer);
            }
        }

        foreach (GameObject racer in nearestRacers) racer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Destroy(powerup);
        yield return new WaitForSeconds(3f);
        foreach (GameObject racer in nearestRacers) racer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
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
