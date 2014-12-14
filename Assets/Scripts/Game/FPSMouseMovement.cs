using UnityEngine;
using System.Collections;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme FPS Mouse Movement
 */
public class FPSMouseMovement : MonoBehaviour
{

    public float sensitivity;
    public float maximumY;
    public float minimumY;

    //For our Up and Down Movement.
    float rotationY = 0f;

    void Update()
    {
        //Right and left
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        //Add our up or down movement
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        //Determine a value between a minimum float and maximum float value.
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        //Set our eulerAngles to our new rotation vector.
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
}
