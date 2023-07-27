using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trick : MonoBehaviour
{
    private Rigidbody2D rb2d;
    
    private bool rotate = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        this.enabled = false;       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) Debug.Log(transform.eulerAngles.z);

        rotate = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (rotate) transform.Rotate(0f, 0f, 12f, Space.Self);
    }
}
