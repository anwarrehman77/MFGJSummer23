using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{
    [SerializeField]
    float lifetime = 3f;

    float timeSinceInstantiated = 0f;

    private void Update()
    {
        timeSinceInstantiated += Time.deltaTime;

        if (timeSinceInstantiated >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
