using UnityEngine;
using System.Collections;

public class DisableCamera : MonoBehaviour
{

    // Use this for initialization
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
