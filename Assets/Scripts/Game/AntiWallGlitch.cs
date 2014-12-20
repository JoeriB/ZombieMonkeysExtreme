using UnityEngine;
using System.Collections;

public class AntiWallGlitch : MonoBehaviour
{

    public bool enabled;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Wall")
            Debug.Log("nothing");
    }
}
