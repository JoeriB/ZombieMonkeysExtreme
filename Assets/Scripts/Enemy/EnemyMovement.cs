using UnityEngine;
using System.Collections;
using System;
/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Enemy Movement: Handles our enemies moving/patrolling
 */
public class EnemyMovement : MonoBehaviour
{
    [Serializable]
    public class Patrol
    {
        public float patrolRadius;
        public Transform patrolPoint;
    }
    [Serializable]
    public class Movement
    {
        public float patrolSpeed;
        public float chaseSpeed;
        public float gravity;
    }
    public float sightDistance;

    public Patrol patrol;
    public Movement movement;
    public Animator animator;

    private CharacterController controller;
    private GameObject player;
    private bool chasing = false;
    private Vector3 randomPatrolPoint;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        randomPatrolPoint = patrol.patrolPoint.position + UnityEngine.Random.insideUnitSphere * patrol.patrolRadius;
        //TODO: Animations, Work out patrolling, look at target when chasing.
    }
    void Update()
    {
        if (!chasing)
            patrolArea();
        //else
        //    chasePlayer();
    }

    private void chasePlayer()
    {
        //Calculate where our player is.
        Vector3 movDiff = player.transform.position - transform.position;
        //Movement speed calculation
        Vector3 movDir = movDiff.normalized * movement.chaseSpeed * Time.deltaTime;
        //Apply Gravity
        movDir.y -= movement.gravity;
        //Move our enemy
        controller.Move(movDir);
    }

    private void patrolArea()
    {
        Vector3 target = randomPatrolPoint - transform.position;
        Debug.Log(target);

        Vector3 moveDirection = target.normalized;

        moveDirection *= movement.patrolSpeed;
        moveDirection.y -= movement.gravity;


        if (target.x < 0.5 && target.z < 0.5)
        {
            Debug.Log("nieuwe patrol point");
            randomPatrolPoint = patrol.patrolPoint.position + UnityEngine.Random.insideUnitSphere * patrol.patrolRadius;
        }
        else
            controller.Move(moveDirection * Time.deltaTime);
    }
}
