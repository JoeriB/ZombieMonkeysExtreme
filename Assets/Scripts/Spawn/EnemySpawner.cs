using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour, SpawnManager
{
    public GameObject spawnPrefab;
    public float spawnTime = 3f;
    public float spawnRadius = 10f;
    public int maxSpawnsPerPoint = 40;
    public Transform[] spawnPoints;
    public GameObject parent;

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
    }
}
