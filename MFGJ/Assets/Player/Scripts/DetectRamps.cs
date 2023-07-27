using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRamps : MonoBehaviour
{
    public float launchForce = 50f;

    Rigidbody2D rb2d;
    PlayerMovement playerMovement;
    Trick playerTrick;
    float targetYPos;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerTrick = GetComponent<Trick>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ramps")
        {
            StartCoroutine(TakeOff());
        }
    }

    IEnumerator TakeOff()
    {
        // Can delete following line to make speed conservative
        rb2d.velocity = new Vector2(0f, 0f);
        targetYPos = transform.position.y;
        playerMovement.enabled = false;
        playerTrick.enabled = true;
        rb2d.gravityScale = 1f;
        rb2d.AddForce(new Vector2(1f, 1f) * launchForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.02f);
        yield return new WaitUntil(() => transform.position.y <= targetYPos);
        
        rb2d.gravityScale = 0f;
        playerMovement.enabled = true;
        playerTrick.enabled = false;
    }
}
