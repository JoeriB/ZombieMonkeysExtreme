using UnityEngine;
using System.Collections;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme FPS Player Movement
 */

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(FPSMouseMovement))]
public class FPSPlayerMovement : MonoBehaviour
{
    //Speed when walking
    public float walkSpeed = 5f;
    //Speed when crouching
    public float crouchSpeed = 3f;
    //Speed when running
    public float runSpeed = 10f;
    //How high we can jump
    public float jumpHeight = 2f;
    //Gravity
    public float gravity = 20f;
    //Crouch Radius for height adjustment
    public float crouchHeight;
    //How smooth is the transition between standing up/crouching
    public float crouchSmoothness = 10f;

    //Our charactercontroller
    private CharacterController controller;
    //Where are we going
    private Vector3 moveDirection = Vector3.zero;
    //Our current speed;
    private float speed;
    //initial character controller radius:
    private bool bounce;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");


        if (controller.isGrounded)
        {
            if (Input.GetButton("Crouch"))
            {
            }
            speed = (Input.GetButton("Walk")) ? walkSpeed : runSpeed;

            moveDirection = new Vector3(inputX, 0, inputY);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                Debug.Log("Jumping");
                moveDirection.y = jumpHeight;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


    }

    public void ApplyCharacterDetails(CharacterDetails details)
    {
        walkSpeed = details.walkSpeed;
        crouchSpeed = details.crouchSpeed;
        runSpeed = details.runSpeed;
    }
}
//var lastHeight = controller.height;
//controller.height = Mathf.Lerp(controller.height, currentHeight, crouchSmoothness * Time.deltaTime);
//var position = transform.position;
//position.y = (controller.height - lastHeight) / 2;

//transform.position = position;
//take your last radius
//var lastradius = controller.radius;
////adjust the character controller radius for crouching
//controller.radius = Mathf.Lerp(controller.radius, radius, crouchSmoothness * Time.deltaTime);
////take current position to alter it later.
//var position = transform.position;
////use this calculation to prevent from falling through the floor ... -.-
//var calculatedifference = (controller.radius - lastradius) / 2;
//if (calculatedifference > 0)
//    position.y += calculatedifference;
////make you stand up after crouch
//transform.position = position;