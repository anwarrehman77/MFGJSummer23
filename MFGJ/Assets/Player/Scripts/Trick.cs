using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trick : MonoBehaviour
{
    public int rotations = 0;
    
    private Rigidbody2D rb2d;

    [SerializeField]
    private float rotateSpeed = 18f;
    private bool rotate = false;
    private bool shouldCount = false;
    private float rotationEpsilon = 60f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.eulerAngles.z >= 360 - rotationEpsilon) && shouldCount)
        {
            rotations++;
            shouldCount = false;
        }
        else if ((transform.eulerAngles.z <= rotationEpsilon))
        {
            shouldCount = true;
        }

        rotate = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (rotate) transform.Rotate(0f, 0f, rotateSpeed, Space.Self);
    }
}
