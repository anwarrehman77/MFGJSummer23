using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    private float lifetime = 10f;
    private float timeSinceInstantiated;

    void Update()
    {
        timeSinceInstantiated += Time.deltaTime;

        if (timeSinceInstantiated >= lifetime) Destroy(gameObject);
    }
}
