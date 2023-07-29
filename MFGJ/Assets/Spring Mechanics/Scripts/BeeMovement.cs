using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    private float boundSizeX = 3f;
    private float maxYUp = 15f;
    private float speedX = 3f;
    private float speedY = 1.3f;
    private float nextDirectionSwapTime = 0f;
    private float directionChangeCooldown, startingX, startingY;
    private int direction = 1;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        directionChangeCooldown = Random.Range(1f, 2f);
        startingY = transform.position.y;
        startingX = transform.position.x;
        direction = Random.value < 0.5f ? -1 : 1;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextDirectionSwapTime)
        {
            direction *= -1;
            directionChangeCooldown = Random.Range(1f, 2f);
            nextDirectionSwapTime += directionChangeCooldown;
        }
        else if ((transform.position.x >= startingX + boundSizeX) || (transform.position.x <= startingX - boundSizeX)) direction *= -1;

        if (transform.position.y >= startingY + maxYUp) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speedX * direction, speedY);
    }
}
