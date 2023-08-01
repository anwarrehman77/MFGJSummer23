using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class HandlePowerups : MonoBehaviour
{
    [SerializeField]
    private GameObject[] oilSpillPrefabs = new GameObject[4];
    [SerializeField]
    private GameObject exhaustPoint, snowballPrefab, inkParticles;
    private GameObject powerup;
    private PlayerMovement movement;
    private PlayerHealth health;
    private Rigidbody2D rb2d;
    private bool hasSnowball = false;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && hasSnowball)
        {
            LaunchSnowball();
            hasSnowball = !hasSnowball;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        powerup = col.gameObject;
        
        switch (col.gameObject.tag)
        {
            case "SpeedBoost": 
            StartCoroutine(ChangeSpeed(1.5f));
            break;
            case "Debuff": 
            StartCoroutine(ChangeSpeed(0.5f));
            break;
            case "Upsize": 
            StartCoroutine(ChangeSize(2f));
            break;
            case "Downsize": 
            StartCoroutine(ChangeSize(0.5f));
            break;
            case "OilSpill": 
            StartCoroutine(SpillOil());
            break;
            case "Invincibility": 
            StartCoroutine(MakeInvincible());
            break;
            case "Boom": 
            ExplodeNearestRacer();
            break;
            case "Rehydrate": 
            Rehydrate();
            break;
            case "Flower": 
            StartCoroutine(StunRacers());
            break;
            case "Bee": 
            StartCoroutine(ChangeSpeed(2.5f));
            break;
            case "Candy":
            health.hydration /= 4;
            StartCoroutine(ChangeSpeed(5f));
            break;
            case "IceCube":
            StartCoroutine(FreezePlayer());
            break;
            case "Present":
            int presentType = UnityEngine.Random.Range(0, 2);
            switch(presentType)
            {
                case 0:
                hasSnowball = true;
                Destroy(powerup);
                break;
                case 1:
                Instantiate(inkParticles, transform.position, Quaternion.identity);
                Destroy(powerup);
                break;
            }
            break;
            case "EggBasket":
            int powerups = UnityEngine.Random.Range(0, 9);
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
            break;
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

        if (nearestRacer != null) nearestRacer.GetComponent<RacerHealth>().TakeDamage(int.MaxValue);
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

    void LaunchSnowball()
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
        Vector2 targetDirection = ((Vector2)nearestRacer.transform.position - (Vector2)exhaustPoint.transform.position).normalized;
        GameObject newSnowball = Instantiate(snowballPrefab, exhaustPoint.transform.position, Quaternion.identity);
        Rigidbody2D snowballrb = newSnowball.GetComponent<Rigidbody2D>();
        snowballrb.velocity = targetDirection * 20f;
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

    IEnumerator SpillOil()
    {
        Destroy(powerup);
        InvokeRepeating("SpawnOilSpill", 0f, 0.05f);
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

    IEnumerator FreezePlayer()
    {
        movement.enabled = false;
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(3f);
        movement.enabled = true;
        Destroy(powerup);
    }
}
