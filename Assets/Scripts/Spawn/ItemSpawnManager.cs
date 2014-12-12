using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawnManager : MonoBehaviour
{
    public GameObject[] items;
    public Transform[] spawnPoints;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < items.Length; i++)
            Instantiate(items[i], spawnPoints[i].position, spawnPoints[i].rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
