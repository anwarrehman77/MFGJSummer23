using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByWaypoints : MonoBehaviour
{
    [SerializeField]
    private Waypoints waypoints;
    [Range(2f, 5f)]
    [SerializeField]
    private float speedX, speedY;

    private Transform currentWaypoint;
    private Rigidbody2D rb2d;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, currentWaypoint.position) < 0.3f) currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        
        speedX = currentWaypoint.position.x >= transform.position.x ? 5f : 2f;
        direction = ((Vector2)currentWaypoint.transform.position - (Vector2)transform.position).normalized;
    }
    
    void FixedUpdate()
    {
        rb2d.velocity = Vector2.Scale(direction, new Vector2(speedX, speedY));
    }
}
