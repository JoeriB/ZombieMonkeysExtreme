using UnityEngine;
using System.Collections;
using System;
/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Enemy Movement: Handles our enemies moving/patrolling
 */
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(EnemyCombat))]
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
        public float rotationSpeed;
        public float gravity;
    }
    public float chaseDistance;

    public Patrol patrol;
    public Movement movement;

    private CharacterController controller;
    private GameObject player;
    private Vector3 randomPatrolPoint;
    private EnemyCombat combat;
    private Animator animator;

    void Start()
    {
        //Character Controller
        controller = GetComponent<CharacterController>();
        //Our Player
        player = GameObject.FindGameObjectWithTag(TagManager.player);
        //Our Enemy Combat script
        combat = GetComponent<EnemyCombat>();
        //Random Patrol point to start with
        randomPatrolPoint = patrol.patrolPoint.position + UnityEngine.Random.insideUnitSphere * patrol.patrolRadius;
        //TODO: Animations: RUN ANIM/CHASE ANIMS/ATTACK ANIMS/MAKE SURE THEY CAN ATTACK BACK
        animator = GetComponent<Animator>();
        //TODO: Add clock to the game... 
        //TODO: Complete Cleanup + scripts nakijken/commentaar/end screen
    }
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= chaseDistance && distance > combat.enemy.attackDistance)
            chasePlayer();
        if (distance <= combat.enemy.attackDistance)
            combat.HandleCombat();
        if (distance > chaseDistance)
            patrolArea();
    }

    private void chasePlayer()
    {
        //Calculate where our player is.
        Vector3 target = player.transform.position - transform.position;
        //Look at Player
        transform.rotation = LookAtTarget(target);
        //Movement speed calculation
        Vector3 movDir = target.normalized * movement.chaseSpeed * Time.deltaTime;
        //Apply Gravity
        movDir.y -= movement.gravity;
        //Move our enemy
        controller.Move(movDir);
        PlayAnimation(Animator.StringToHash("Chase"));
    }

    private void patrolArea()
    {
        //Get the target vector
        Vector3 target = randomPatrolPoint - transform.position;
        //Look at patrol point
        transform.rotation = LookAtTarget(target);
        //Movement direction to calculate our movement
        Vector3 moveDirection = target.normalized;
        //Adjust the right speed for patrolling
        moveDirection *= movement.patrolSpeed;
        //Apply gravity
        moveDirection.y -= movement.gravity;
        //Magnitude is fucking up somehow so use this to determinate when a new patrol point is needed!
        if (target.x < 0.5 && target.z < 0.5)
        {
            randomPatrolPoint = patrol.patrolPoint.position + UnityEngine.Random.insideUnitSphere * patrol.patrolRadius;
        }
        //Move enemy
        else
        {
            controller.Move(moveDirection * Time.deltaTime);
            PlayAnimation(Animator.StringToHash("Walk"));
        }
    }

    /**
     * Look at a target, this method will rotate our enemy towards a target with a smooth movement.
     */
    private Quaternion LookAtTarget(Vector3 target)
    {
        var newRotation = Quaternion.LookRotation(target);
        newRotation.x = 0;
        newRotation.z = 0;
        return Quaternion.Slerp(transform.rotation, newRotation, movement.rotationSpeed * Time.deltaTime);
    }

    private void PlayAnimation(int animationHash)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).nameHash.Equals(animationHash))
            animator.SetTrigger(animationHash);
    }
}
