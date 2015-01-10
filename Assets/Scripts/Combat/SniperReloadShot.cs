using UnityEngine;
using System.Collections;
/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Sniper Reload Shot; handles our sniper reloading shot + fire sounds
 */

[RequireComponent(typeof(AudioSource))]
public class SniperReloadShot : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip boltOut;
        public float boltOutDelay;
        public AudioClip boltIn;
        public float boltInDelay;
    }
    public Sound sound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ReloadBullet(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(BoltIn());
        StartCoroutine(BoltOut());
    }

    private IEnumerator BoltIn()
    {
        yield return new WaitForSeconds(sound.boltInDelay);
        AudioSource.PlayClipAtPoint(sound.boltIn, transform.position);
    }
    private IEnumerator BoltOut()
    {
        yield return new WaitForSeconds(sound.boltOutDelay);
        AudioSource.PlayClipAtPoint(sound.boltOut, transform.position);
    }
}
