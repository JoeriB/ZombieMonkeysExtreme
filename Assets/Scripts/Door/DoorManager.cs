using UnityEngine;
using System.Collections;
using System;

public class DoorManager : MonoBehaviour
{

    [Serializable]
    public class Sound
    {
        public AudioClip openDoorSound;
        public AudioClip closeDoorSound;
    }
    public Animator animator;
    public Sound sound;

    bool canOpen = false;
    bool isDoorOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (canOpen && Input.GetKeyDown("e"))
        {
            if (!isDoorOpen)
            {
                AudioSource.PlayClipAtPoint(sound.openDoorSound, transform.position);
                animator.SetTrigger(Animator.StringToHash("Open"));
            }
            else
            {
                AudioSource.PlayClipAtPoint(sound.closeDoorSound, transform.position);
                animator.SetTrigger(Animator.StringToHash("Close"));
            }
            isDoorOpen = !isDoorOpen;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        canOpen = true;
    }
    void OnTriggerExit(Collider collider)
    {
        canOpen = false;
    }
}
