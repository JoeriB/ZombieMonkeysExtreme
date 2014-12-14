using UnityEngine;
using System.Collections;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Disable Camera
 */
public class DisableCamera : MonoBehaviour
{

    // Use this for initialization
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }
}
