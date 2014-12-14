using UnityEngine;
using System.Collections;
/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Enemy Movement: Handles our enemies moving/patrolling
 */
public class EnemyMovement : MonoBehaviour
{
    public float speed = 9f;
    public float gravity = 9.81f;
    public float sightDistance = 1000f;

    public Transform[] waypoints;
    public Animator animator;
    public CharacterController controller;
    public GameObject player;

    private bool chasing = false;
    private int currentWayPointIndex;

    void Start()
    {
        //TODO: Animations, Work out patrolling, look at target when chasing.
        currentWayPointIndex = 0;
    }
    void Update()
    {
        if (!chasing)
            patrol();
        else
            chasePlayer();
    }

    private void chasePlayer()
    {
        //Calculate where our player is.
        Vector3 movDiff = player.transform.position - transform.position;
        //Movement speed calculation
        Vector3 movDir = movDiff.normalized * speed * Time.deltaTime;
        //Apply Gravity
        movDir.y -= gravity;
        //Move our enemy
        controller.Move(movDir);
    }

    private void patrol()
    {
        Vector3 target = waypoints[currentWayPointIndex].position;
        Vector3 moveDirection = target - transform.position;
        if (moveDirection.magnitude < 0.5)
        {
            transform.position = target;
            currentWayPointIndex++;
        }
        else
        {
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            //animator.SetFloat("Speed", speed);
        }
        if (currentWayPointIndex >= waypoints.Length)
        {
            currentWayPointIndex = 0;
        }
    }
}
