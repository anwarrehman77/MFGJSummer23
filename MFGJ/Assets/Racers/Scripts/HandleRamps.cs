using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRamps : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private MoveByWaypoints racerMovement;
    private BoxCollider2D wheelsCollider;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        racerMovement = GetComponent<MoveByWaypoints>();
        wheelsCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ramps") 
        {
            StartCoroutine(TakeOff());
        }
    }

    IEnumerator TakeOff()
    {
        float takeOffTime = Time.time;

        rb2d.velocity = Vector2.zero; // Can delete this line to conserve current momentum
        float targetYPos = transform.position.y;
        
        racerMovement.enabled = false;
        wheelsCollider.enabled = false;
        
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.gravityScale = 1f;
        
        rb2d.AddForce(new Vector2(1f, 1f) * 5, ForceMode2D.Impulse);

        yield return new WaitUntil(() => (transform.position.y <= targetYPos) && (Time.time > takeOffTime + 0.02f));
        
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        rb2d.gravityScale = 0f;

        racerMovement.enabled = true;
        wheelsCollider.enabled = true;
    }
}
