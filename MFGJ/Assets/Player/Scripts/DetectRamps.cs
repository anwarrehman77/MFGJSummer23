using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRamps : MonoBehaviour
{
    // Variables
    public float launchForce = 50f;

    Rigidbody2D rb2d;
    PlayerMovement playerMovement;
    Trick playerTrick;
    BoxCollider2D wheelsCollider;
    PlayerHealth playerHealth;

    float targetYPos;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerTrick = GetComponent<Trick>();
        wheelsCollider = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ramps")
        {
            StartCoroutine(TakeOff());
        }
    }

    void CheckRot()
    {
        if ((transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 60) || (transform.eulerAngles.z >= 300 && transform.eulerAngles.z <= 359.9999f)) transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else playerHealth.Die();
    }

    IEnumerator TakeOff()
    {
        rb2d.velocity = new Vector2(0f, 0f); // Can delete this line to conserve current momentum
        targetYPos = transform.position.y;
        
        playerMovement.enabled = false;
        wheelsCollider.enabled = false;
        playerTrick.enabled = true;
        
        rb2d.gravityScale = 1f;
        
        rb2d.AddForce(new Vector2(1f, 1f) * launchForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.02f);
        yield return new WaitUntil(() => transform.position.y <= targetYPos);
        
        rb2d.gravityScale = 0f;
        playerTrick.rotations = 0;

        playerMovement.enabled = true;
        playerTrick.enabled = false;
        wheelsCollider.enabled = true;

        CheckRot();
    }
}
