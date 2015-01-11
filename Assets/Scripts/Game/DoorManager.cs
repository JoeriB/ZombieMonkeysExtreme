using UnityEngine;
using System;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Door Manager: Handles how and when a door can be opened
 */
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

    private bool canOpen = false;
    private bool doorOpen = false;

    void Update()
    {
        if (canOpen && Input.GetKeyDown("e"))
        {
            if (!doorOpen)
            {
                AudioSource.PlayClipAtPoint(sound.openDoorSound, transform.position);
                animator.SetTrigger(Animator.StringToHash("Open"));
            }
            else
            {
                AudioSource.PlayClipAtPoint(sound.closeDoorSound, transform.position);
                animator.SetTrigger(Animator.StringToHash("Close"));
            }
            doorOpen = !doorOpen;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name.Equals(TagManager.player))
            canOpen = true;
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.name.Equals(TagManager.player))
            canOpen = false;
    }

    public bool isDoorOpen()
    {
        return doorOpen;
    }
}
