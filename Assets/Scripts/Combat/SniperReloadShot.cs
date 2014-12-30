using UnityEngine;
using System.Collections;

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

    public void ReloadBullet()
    {
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
