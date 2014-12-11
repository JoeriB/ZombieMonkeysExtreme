using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] waypoints;
    public Animator animator;
    public CharacterController controller;
    public GameObject player;
    public float sightDistance = 1000f;

    private bool chasing = false;
    private int currentWayPointIndex;

    void Start()
    {
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
        Vector3 moveDirection = player.transform.position - transform.position;

        controller.Move(moveDirection.normalized * speed * Time.deltaTime);
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
