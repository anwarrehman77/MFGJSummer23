using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    private float boundsY = 50f;
    private float startingY;

    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= startingY - boundsY)
        {
            Destroy(gameObject);
        }
    }
}
