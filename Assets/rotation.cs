using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public float angularSpeed;
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        speed = rb.velocity.magnitude;
        angularSpeed = rb.angularVelocity.magnitude;

        rb.AddTorque(Vector3.forward);
    }
}
