using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Item Spawner: handles all the item spawning on the map
 */
public class ItemSpawner : MonoBehaviour, SpawnManager
{
    [SerializeField]
    private GameObject[] items;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject parent;
    private int counter;

    void Start()
    {
        foreach (GameObject item in items)
        {
            SpawnObject(item);
        }
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
