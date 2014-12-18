using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform waypoint;

    public NavMeshAgent nav;
    //// Use this for initialization
    //void Start()
    //{
    //    nav = GetComponent<NavMeshAgent>();
    //}

    // Update is called once per frame
    void Update()
    {
        nav.speed = 0.5f;
        nav.destination = waypoint.position;
    }
}
