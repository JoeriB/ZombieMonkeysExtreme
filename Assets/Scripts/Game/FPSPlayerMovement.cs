using UnityEngine;
using System.Collections;
using System;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme FPS Player Movement
 */

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(FPSMouseMovement))]
public class FPSPlayerMovement : MonoBehaviour
{
    [Serializable]
    public class Footstep
    {
        public AudioClip rightStep;
        public AudioClip leftStep;
        public float runCooldown;
        public float walkCooldown;
    }
    [SerializeField]
    private Footstep footStep;
    //Speed when walking
    public float walkSpeed = 5f;
    //Speed when crouching
    public float crouchSpeed = 3f;
    //Speed when running
    public float runSpeed = 10f;
    //How high we can jump
    public float jumpHeight = 2f;
    //Limit Diagonal Speed
    public bool limitDiagonalSpeed;
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
    //Footstep cooldown timer
    private float stepCooldown;
    //Step counter for left/right sounds
    private int stepCounter;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        stepCooldown += Time.deltaTime;
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .70f : 1.0f;

        if (controller.isGrounded)
        {
            speed = (Input.GetButton("Walk")) ? walkSpeed : runSpeed;

            moveDirection = new Vector3(inputX * inputModifyFactor, 0, inputY * inputModifyFactor);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (inputX != 0.0f || inputY != 0.0f)
            {
                if (stepCooldown >= ((speed == runSpeed) ? footStep.runCooldown : footStep.walkCooldown))
                {
                    stepCooldown = 0f;
                    GetComponent<AudioSource>().PlayOneShot((stepCounter++ % 2 == 0) ? footStep.rightStep : footStep.leftStep);
                }
            }

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpHeight;
            //Fixed random hopping when looking up...!
            else
                moveDirection.y = Terrain.activeTerrain.transform.position.y;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void ApplyCharacterDetails(CharacterDetails details)
    {
        walkSpeed = details.GetWalkSpeed();
        crouchSpeed = details.GetCrouchSpeed();
        runSpeed = details.GetRunSpeed();
    }
}