using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballLauncher : MonoBehaviour
{
    public GameObject snowballPrefab;
    private GameObject[] racers;
    public float launchSpeed = 20f;
    private Transform targetRacer;
    private bool isLaunchingSnowball = false;

    void Start()
    {
        racers = GameObject.FindGameObjectsWithTag("Racer");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isLaunchingSnowball)
        {
            if (racers.Length > 0)
            {
                targetRacer = GetNearestRacer();
                LaunchSnowball();
            }
            else
            {
                Debug.LogWarning("no racers");
            }
        }
    }

    void LaunchSnowball()
    {
        isLaunchingSnowball = true; 

        if (targetRacer != null)
        {
            Vector3 launchPosition = GetOilPointPosition();

            GameObject snowballInstance = Instantiate(snowballPrefab, launchPosition, Quaternion.identity);

            Rigidbody2D snowballRigidbody = snowballInstance.GetComponent<Rigidbody2D>();
            Vector2 targetDirection = ((Vector2)targetRacer.position - (Vector2)launchPosition).normalized;
            snowballRigidbody.velocity = targetDirection * launchSpeed;
        }
        StartCoroutine(ResetLaunchFlag());
    }

    IEnumerator ResetLaunchFlag()
    {
        yield return new WaitForSeconds(3f); // adjustable snowball delay
    }

    Vector3 GetOilPointPosition()
    {
        GameObject oilPoint = GameObject.Find("OilPoint");
        if (oilPoint != null)
        {
            return oilPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("OilPoint not found!");
            return transform.position; 
        }
    }

    Transform GetNearestRacer()
    {
        if (racers.Length == 0)
            return null;

        Transform nearestRacer = racers[0].transform;
        float nearestDistanceSqr = Vector3.SqrMagnitude(nearestRacer.position - transform.position);

        for (int i = 1; i < racers.Length; i++)
        {
            float distanceSqr = Vector3.SqrMagnitude(racers[i].transform.position - transform.position);
            if (distanceSqr < nearestDistanceSqr)
            {
                nearestRacer = racers[i].transform;
                nearestDistanceSqr = distanceSqr;
            }
        }
        return nearestRacer;
    }
}