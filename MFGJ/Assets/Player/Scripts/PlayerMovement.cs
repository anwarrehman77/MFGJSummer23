using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedBiasX = 10f;
    [SerializeField]
    private float speedY = 2f, speedX = 5f;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speedX * Input.GetAxisRaw("Horizontal") + speedBiasX, speedY * Input.GetAxisRaw("Vertical"));
    }
}
