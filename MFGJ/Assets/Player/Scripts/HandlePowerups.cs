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
    private bool hasSnowball = false;
    private PlayerMovement movement;
    private Rigidbody2D rb2d;
    private PlayerHealth health;
    private GameObject powerup;
  
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
            case "Candy":
            health.hydration /= 4;
            StartCoroutine(ChangeSpeed(5f));
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

    IEnumerator FreezePlayer()
    {
        movement.enabled = false;
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(3f);
        movement.enabled = true;
        Destroy(powerup);
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
        snowballrb.velocity = targetDirection * 13f;
    }
}