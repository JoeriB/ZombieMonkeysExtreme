using UnityEngine;
using System.Collections;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme FPS Player Movement
 */

[RequireComponent(typeof(CharacterController))]
public class FPSPlayerMovement : MonoBehaviour
{
    //Speed when walking
    public float walkSpeed = 7f;
    //Speed when crouching
    public float crouchSpeed = 5f;
    //Speed when running
    public float runSpeed = 10f;
    //How high we can jump
    public float jumpHeight = 2f;
    //Gravity
    public float gravity = 20f;
    //Crouch Radius for height adjustment
    public float crouchHeight = 0.5f;
    //How smooth is the transition between standing up/crouching
    public float crouchSmoothness = 10f;

    //Our charactercontroller
    private CharacterController controller;
    //Where are we going
    private Vector3 moveDirection = Vector3.zero;
    //Our current speed;
    private float speed;
    //initial character controller radius:
    private float initialRadius;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        initialRadius = controller.radius;
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(inputX, 0, inputY);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y += jumpHeight * 10;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Update()
    {
        var radius = initialRadius;
        speed = (Input.GetButton("Walk")) ? walkSpeed : runSpeed;
        //Check for crouching
        if (Input.GetButton("Crouch"))
        {
            speed = crouchSpeed;
            radius = crouchHeight;
        }
        //Take your last radius
        var lastRadius = controller.radius;
        //Adjust the character controller radius for crouching
        controller.radius = Mathf.Lerp(controller.radius, radius, crouchSmoothness * Time.deltaTime);
        //take current position to alter it later.
        var position = transform.position;
        //Use this calculation to prevent from falling through the floor ... -.-
        var calculateDifference = (controller.radius - lastRadius) / 2;
        if (calculateDifference > 0)
            position.y += calculateDifference;

        //Make you stand up after crouch
        transform.position = position;
    }
}
