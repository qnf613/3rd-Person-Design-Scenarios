using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacterController : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 10;
    [SerializeField] private float maxSpeed = 2;
    [SerializeField] [Tooltip("How fast the player turns. 0 = no turning, 1 = instant turning")] [Range (0,1)] private float turnSpeed = .1f;
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
        //detect the input direction
        var inputDirection = new Vector3(input.x, 0, input.y);
        //detect the direction that camera is looking at
        Vector3 camFlattenedFoward = Camera.main.transform.forward;
        camFlattenedFoward.y = 0;
        var camRotarion = Quaternion.LookRotation(camFlattenedFoward);
        //calculate the direction that where player object suppose to go
        Vector3 camRelativeInputDir = camRotarion * inputDirection;

        //change the physics material depending on player's movement status
        collider.material = inputDirection.magnitude > 0 ? moving : stopping;
        //line 34 is simplified code writing of the line 36 to 43
        //if (inputDirection.magnitude > 0)
        //{
        //    collider.material = moving;
        //}
        //else
        //{
        //    collider.material = stopping;
        //}

        //add force to the player object in direction the camera is looking at
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(camRelativeInputDir * accelerationForce, ForceMode.Acceleration);
        }

        if (inputDirection.magnitude > 0)
        {
            //give us back the direction of player's moving to get the player to face the way they're moving
            var targetRotation = Quaternion.LookRotation(camRelativeInputDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed);

        }

    }


    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
}
