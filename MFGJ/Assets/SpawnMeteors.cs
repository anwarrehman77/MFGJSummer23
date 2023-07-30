using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteors : MonoBehaviour
{
    [SerializeField]
    GameObject meteor;

    private float spawnCooldown;
    private float nextSpawnTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Instantiate(meteor, transform.position, Quaternion.identity);
            spawnCooldown = Random.Range(2f, 5f);
            nextSpawnTime = Time.time + spawnCooldown;
        }
    }
}
