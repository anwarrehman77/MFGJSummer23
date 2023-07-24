using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePowerups : MonoBehaviour
{
    PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.name == "SpeedBoost")
        {
            Debug.Log("Speeding Up!");
            StartCoroutine(ChangeSpeed(1.2f));
        }
        else if (other.gameObject.name == "Mega")
        {

        }
        else if (other.gameObject.name == "Oil Spill")
        {

        }
        else if (other.gameObject.name == "Invincibility")
        {

        }
        else if (other.gameObject.name == "Debuff")
        {
            Debug.Log("Slowing Down!");
            StartCoroutine(ChangeSpeed(0.8f));
        }
    }

    IEnumerator ChangeSpeed(float speedFactor)
    {
        movement.speedBiasX *= speedFactor;
        yield return new WaitForSeconds(3f);
        movement.speedBiasX /= speedFactor;
    }
}
