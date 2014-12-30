using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Weapon Combat: Handles the combat system for all our weapons.
 */
public class WeaponCombat : MonoBehaviour
{
    [Serializable]
    public class Weapon
    {
        public int bulletDamage = 100;
        public int maxBullets = 200;
        public int maxBulletsPerMag = 40;
        public int currentBulletsInMag = 0;
        public int currentTotalBullets = 0;
        public int shootRange = 100;

        public float timeBetweenBullets = 0.15f;
        public float timeBetweenReload = 2.0f;
        public float timeBeforeMagBackIn = 1.0f;
        public float timeBeforeSpecialSound = 1.2f;
        public float aimLineDisplayTime = 1f;
        public WeaponType weaponType;
    }
    [Serializable]
    public class WeaponConfig
    {
        public AudioClip reloadMagOutSound;
        public AudioClip reloadMagInSound;
        public AudioClip reloadThirdSound;
        public AudioClip fireSound;
        public AudioClip drawSound;
        public AudioClip zoomSound;
        public ParticleSystem fireParticle;
        public Sprite weaponSprite;
    }

    public Weapon weapon;
    public WeaponConfig weaponConfig;
    public Animator animator;

    private Text weaponText;
    private Image weaponImage;
    private ParticleSystem shootParticle;
    private LineRenderer aimLine;
    private float timer;
    private float reloadTimer;

    void Start()
    {
        StartUpWeapon();
    }

    public void Initiate()
    {
        StartUpWeapon();
    }

    private void StartUpWeapon()
    {
        weaponText = GameObject.FindGameObjectWithTag(TagManager.weaponText).GetComponent<Text>();
        weaponImage = GameObject.FindGameObjectWithTag(TagManager.weaponImage).GetComponent<Image>();

        weapon.currentTotalBullets = weapon.maxBullets - weapon.maxBulletsPerMag;
        weapon.currentBulletsInMag = weapon.maxBulletsPerMag;

        weaponImage.enabled = true;
        weaponImage.sprite = weaponConfig.weaponSprite;

        if (weapon.weaponType != WeaponType.KNIFE)
        {
            GameObject particles = GameObject.FindGameObjectWithTag(TagManager.gunParticle);
            shootParticle = particles.GetComponent<ParticleSystem>();
            aimLine = particles.GetComponent<LineRenderer>();
        }
        UpdateWeaponText();
    }

    void Update()
    {
        timer += Time.deltaTime;
        reloadTimer += Time.deltaTime;

        if (Input.GetKeyDown("r") && reloadTimer >= weapon.timeBetweenReload)
            StartCoroutine(Reload());

        if (Input.GetButton("Fire1"))
        {
            if (weapon.weaponType != WeaponType.KNIFE)
                StartCoroutine(Shoot());
            else
                StartCoroutine(Knife());
        }
        if (Input.GetButton("Zoom") && weapon.weaponType == WeaponType.SNIPER)
            Zoom();
        else
            Camera.main.fieldOfView = 60f;

        if (aimLine != null && timer >= weapon.timeBetweenBullets * weapon.aimLineDisplayTime && weapon.weaponType != WeaponType.KNIFE)
            aimLine.enabled = false;
    }
    #region shooting/knifing/zooming

    public void Zoom()
    {
        Camera.main.fieldOfView = 12f;
    }
    IEnumerator Shoot()
    {
        if (timer >= weapon.timeBetweenBullets && reloadTimer >= weapon.timeBetweenReload && weapon.currentBulletsInMag > 0)
        {
            timer = 0f;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            AudioSource.PlayClipAtPoint(weaponConfig.fireSound, transform.position);

            PlayAnimation(GetWeaponShootTrigger());
            if (weapon.weaponType == WeaponType.SNIPER)
            {
                GetComponent<SniperReloadShot>().ReloadBullet();
            }
            //shootParticle.Play();
            aimLine.enabled = true;
            aimLine.SetPosition(0, transform.position);

            if (Physics.Raycast(ray, out hit, weapon.shootRange))
            {
                EnemyCombat enemy = hit.collider.GetComponent<EnemyCombat>();
                if (enemy != null)
                    enemy.HandleIncomingDamage(weapon.bulletDamage);
                aimLine.SetPosition(1, hit.point);
            }
            else
            {
                aimLine.SetPosition(1, ray.origin + ray.direction * weapon.shootRange);
            }
            weapon.currentBulletsInMag--;
            UpdateWeaponText();
            if (weapon.currentBulletsInMag <= 0)
            {
                StartCoroutine(Reload());
            }
            //Delay van bullet
            yield return new WaitForSeconds(weapon.timeBetweenBullets);
        }
    }

    IEnumerator Knife()
    {
        if (timer >= weapon.timeBetweenBullets)
        {
            timer = 0f;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            AudioSource.PlayClipAtPoint(weaponConfig.fireSound, transform.position);

            PlayAnimation(GetWeaponShootTrigger());
            if (Physics.Raycast(ray, out hit, weapon.shootRange))
            {
                EnemyCombat enemy = hit.collider.GetComponent<EnemyCombat>();
                if (enemy != null)
                    enemy.HandleIncomingDamage(weapon.bulletDamage);
            }
            UpdateWeaponText();
        }
        yield return new WaitForSeconds(weapon.timeBetweenBullets);
    }
    #endregion

    #region reloading

    IEnumerator ReloadSpecialSound()
    {
        yield return new WaitForSeconds(weapon.timeBeforeSpecialSound);
        AudioSource.PlayClipAtPoint(weaponConfig.reloadThirdSound, transform.position);
    }

    IEnumerator ReloadMagBackInSound()
    {
        yield return new WaitForSeconds(weapon.timeBeforeMagBackIn);
        AudioSource.PlayClipAtPoint(weaponConfig.reloadMagInSound, transform.position);
    }
    IEnumerator Reload()
    {
        if (weapon.currentTotalBullets > 0 && weapon.currentBulletsInMag < weapon.maxBulletsPerMag)
        {
            StartCoroutine(ReloadMagBackInSound());
            if (weaponConfig.reloadThirdSound != null)
            {
                StartCoroutine(ReloadSpecialSound());
            }

            reloadTimer = 0f;
            AudioSource.PlayClipAtPoint(weaponConfig.reloadMagOutSound, transform.position);

            int bulletsLeftInMag = weapon.maxBulletsPerMag - weapon.currentBulletsInMag;
            if (bulletsLeftInMag > weapon.currentTotalBullets)
            {
                bulletsLeftInMag = weapon.currentTotalBullets;
            }
            weapon.currentBulletsInMag += bulletsLeftInMag;
            weapon.currentTotalBullets -= bulletsLeftInMag;

            PlayAnimation(GetWeaponReloadTrigger());

            yield return new WaitForSeconds(weapon.timeBetweenReload);
            UpdateWeaponText();
        }
    }
    #endregion

    #region animations + weapontext update
    public void DrawWeapon()
    {
        PlayAnimation(GetWeaponDrawTrigger());
        AudioSource.PlayClipAtPoint(weaponConfig.drawSound, transform.position);
    }
    public void UpdateWeaponText()
    {
        if (weaponText != null)
        {
            weaponText.text = name + Environment.NewLine;
            if (weapon.weaponType != WeaponType.KNIFE)
                weaponText.text += "Ammo: " + weapon.currentBulletsInMag + "/" + weapon.currentTotalBullets;

            weaponImage.sprite = weaponConfig.weaponSprite;
        }
    }
    public void PlayAnimation(int animationHash)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).nameHash.Equals(animationHash))
            animator.SetTrigger(animationHash);
    }

    private int GetWeaponShootTrigger()
    {
        return Animator.StringToHash("Shoot");
    }
    private int GetWeaponReloadTrigger()
    {
        return Animator.StringToHash("Reload");
    }
    private int GetWeaponDrawTrigger()
    {
        return Animator.StringToHash("Draw");
    }
    #endregion
}
