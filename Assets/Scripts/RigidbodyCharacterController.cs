﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacterController : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 10;
    [SerializeField] private float maxSpeed = 2;

    private new Rigidbody rb;
    private Vector2 input;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        var inputDirection = new Vector3(input.x, 0, input.y);
        
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(inputDirection * accelerationForce);
        }
    }


    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

    }
}
