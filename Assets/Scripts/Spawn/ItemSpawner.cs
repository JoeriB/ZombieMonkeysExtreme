using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour, SpawnManager
{
    public GameObject[] items;
    public Transform[] spawnPoints;
    public GameObject parent;

    int counter;

    void Start()
    {
        foreach (GameObject item in items)
        {
            SpawnObject(item);
        }
        Debug.Log("Spawned " + counter + " Items..");
    }

    public void SpawnObject(GameObject spawnObject)
    {
        //Make a new gameObject
        GameObject spawnedItem = Instantiate(spawnObject, spawnPoints[counter].position, spawnPoints[counter].rotation) as GameObject;
        //Add GameObject to right Parent Folder in Hierarchy
        spawnedItem.transform.parent = parent.transform;
        counter++;
    }
}
