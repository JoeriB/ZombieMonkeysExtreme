using UnityEngine;
using System.Collections;
/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme SpawnManager: Interface for all our spawn scripts. Will spawn a GameObject
 */
public interface SpawnManager
{
    void SpawnObject(GameObject spawnObject);
}
