using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RigidbodyCharacterController : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 10;
    [SerializeField] private float maxSpeed = 2;
    [SerializeField] [Tooltip("How fast the player turns. 0 = no turning, 1 = instant turning")] [Range (0,1)] private float turnSpeed = .1f;
    [SerializeField] private PhysicMaterial stopping, moving;

    private new Rigidbody rb;
    private new Collider collider;
    private Vector2 input;
    private Animator animator;
    private readonly int movementInputAnimParam = Animator.StringToHash("movementInput");
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 camRelativeInputDir = GetCameraRlariveInputDir();
        UpdatePhysicsMaterial();
        Move(camRelativeInputDir);
        RotateFacing(camRelativeInputDir);
    }

    /// <summary>
    /// turn the character to face the direction it want to move in
    /// </summary>
    /// <param name="movementDir"></param>
    private void RotateFacing(Vector3 movementDir)
    {
        if (movementDir.magnitude > 0)
        {
            var targetRotation = Quaternion.LookRotation(movementDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed);
        }
    }

    /// <summary>
    /// moves the player in a direction based on its max speed and acceleration
    /// </summary>
    /// <param name="moveDir">the direction to move in.</param>
    private void Move(Vector3 moveDir)
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(moveDir * accelerationForce, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// update the physics material to a low firiction option if the player is trying to move,
    /// or higher friction option if they are trying to stop.
    /// </summary>
    private void UpdatePhysicsMaterial()
    {
        collider.material = input.magnitude > 0 ? moving : stopping;
        //simpler version of these lines of code
        //if (inputDirection.magnitude > 0)
        //{
        //    collider.material = moving;
        //}
        //else
        //{
        //    collider.material = stopping;
        //}
    }

    /// <summary>
    /// uses to input vector to create a cameara relative version
    /// so the player can move based on the camera's foward.
    /// </summary>
    /// <returns>returns the camera relative input direction.</returns>
    private Vector3 GetCameraRlariveInputDir()
    {
        var inputDirection = new Vector3(input.x, 0, input.y);
        Vector3 camFlattenedFoward = Camera.main.transform.forward;
        camFlattenedFoward.y = 0;
        var camRotation = Quaternion.LookRotation(camFlattenedFoward);
        Vector3 camRelativeInputDirToReturn = camRotation * inputDirection;

        return camRelativeInputDirToReturn;
    }

    /// <summary>
    /// this eveny handler is called from the playerInput component
    /// using the new input system.
    /// </summary>
    /// <param name="context">Vector 2 representing move input.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        animator.SetFloat(movementInputAnimParam, input.magnitude);
    }
}
