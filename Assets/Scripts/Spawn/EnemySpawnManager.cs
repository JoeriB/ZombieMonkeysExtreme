using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject spawnPrefab;
    public float spawnTime = 3f;
    public float spawnRadius = 10f;
    public int maxSpawnsPerPoint = 40;
    int counter = 0;
    public Transform[] spawnPoints;
    public GameObject parent;


    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    void SpawnEnemy()
    {
        if (counter++ <= maxSpawnsPerPoint)
        {
            //Random Spawn plek
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            //Random positie pakken in een random radius
            var position = spawnPoints[spawnPointIndex].position + Random.insideUnitSphere * spawnRadius;
            //Hoogte aanpassen zodat we niet met te hoog zijn -> 5 == het terrein staat op -5 daarom standaard er aftrekken
            position.y = Terrain.activeTerrain.SampleHeight(position) - 5;
            //uw monster aanmaken en de positie en rotation klaar zetten.
            GameObject spawnedEnemy = Instantiate(spawnPrefab, position, spawnPoints[spawnPointIndex].rotation) as GameObject;
            //In de juiste hierarchy plakken (Enemies in ons geval)
            spawnedEnemy.transform.parent = parent.transform;

        }
    }
}
