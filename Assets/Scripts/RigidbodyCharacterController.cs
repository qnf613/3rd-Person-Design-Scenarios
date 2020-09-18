using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacterController : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 10;
    [SerializeField] private float maxSpeed = 2;
    [SerializeField] private PhysicMaterial stopping, moving;

    private new Rigidbody rb;
    private new Collider collider;
    private Vector2 input;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        var inputDirection = new Vector3(input.x, 0, input.y);

        collider.material = inputDirection.magnitude > 0 ? moving : stopping;
        //line 26 is simplified code writing of the line 28 to 35
        //if (inputDirection.magnitude > 0)
        //{
        //    collider.material = moving;
        //}
        //else
        //{
        //    collider.material = stopping;
        //}

        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(inputDirection * accelerationForce, ForceMode.Acceleration);
        }
    }


    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
}
