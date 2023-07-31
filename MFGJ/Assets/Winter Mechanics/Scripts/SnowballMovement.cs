using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballMovement : MonoBehaviour
{
    private float lifetime = 10;
    private float timeSinceInstantiated = 0f;

    void Update()
    {
        timeSinceInstantiated += Time.deltaTime;

        if (timeSinceInstantiated >= lifetime) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Racer")
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}
