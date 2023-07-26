using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramps : MonoBehaviour
{
    PlayerMovement playerMovement;
    Rigidbody2D racerRb2d;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerMovement = col.gameObject.GetComponent<PlayerMovement>();
            racerRb2d = col.gameObject.GetComponent<Rigidbody2D>();

            if (playerMovement != null) playerMovement.enabled = false;

            racerRb2d.AddForce(new Vector2(5f, 5f), ForceMode2D.Impulse);
        }
    }
}
