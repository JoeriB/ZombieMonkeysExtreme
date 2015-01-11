using UnityEngine;
using System.Collections;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Enemy Spawner: Handles all the enemy zombie spawns
 */
public class EnemySpawner : MonoBehaviour, SpawnManager
{
    [SerializeField]
    private GameObject spawnPrefab;
    [SerializeField]
    private float spawnRadius = 10f;
    [SerializeField]
    private int maxSpawnsPerPoint = 40;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject parent;

    int spawnPointIndex = 0;

    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            for (int j = 0; j < maxSpawnsPerPoint; j++)
            {
                spawnPointIndex = i;
                SpawnObject(spawnPrefab);
            }
        }
    }

    public void SpawnObject(GameObject spawnObject)
    {
        //Spawn at a random in our sphere
        var position = spawnPoints[spawnPointIndex].position + Random.insideUnitSphere * spawnRadius;
        //Put our object on the right height so it won't float in the air
        position.y = Terrain.activeTerrain.SampleHeight(position) - 5;
        //Make a new GameObject
        GameObject spawnedEnemy = Instantiate(spawnPrefab, position, spawnPoints[spawnPointIndex].rotation) as GameObject;
        //Add GameObject to right Parent Folder in Hierarchy
        spawnedEnemy.transform.parent = parent.transform;
        //Set the right patrol point for the right enemy
        spawnedEnemy.GetComponent<EnemyMovement>().SetPatrolPoint(spawnPoints[spawnPointIndex]);
    }
}
