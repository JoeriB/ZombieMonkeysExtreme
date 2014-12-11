using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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
        public float aimLineDisplayTime = 1f;
        public WeaponType weaponType;
    }
    [Serializable]
    public class WeaponConfig
    {
        public AudioClip reloadSound;
        public AudioClip fireSound;
        public AudioClip drawSound;

        public ParticleSystem fireParticle;
        public Sprite weaponSprite;
    }

    public Weapon weapon;
    public WeaponConfig weaponConfig;
    public Animator animator;

    Text weaponText;
    Image weaponImage;
    ParticleSystem shootParticle;
    LineRenderer aimLine;
    float timer;
    float reloadTimer;


    void Start()
    {
        shootParticle = GetComponentInChildren<ParticleSystem>();
        weaponText = GameObject.FindGameObjectWithTag("WeaponText").GetComponent<Text>();
        weaponImage = GameObject.FindGameObjectWithTag("WeaponImage").GetComponent<Image>();

        weapon.currentTotalBullets = weapon.maxBullets - weapon.maxBulletsPerMag;
        weapon.currentBulletsInMag = weapon.maxBulletsPerMag;

        weaponImage.enabled = true;
        weaponImage.sprite = weaponConfig.weaponSprite;

        if (weapon.weaponType != WeaponType.KNIFE)
        {
            aimLine = shootParticle.GetComponent<LineRenderer>();
            UpdateWeaponText();
        }

    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        reloadTimer += Time.deltaTime;

        if (Input.GetKeyDown("r") && reloadTimer >= weapon.timeBetweenReload)
        {
            StartCoroutine(Reload());
        }
        if (Input.GetButton("Fire1"))
        {
            if (weapon.weaponType != WeaponType.KNIFE)
            {
                StartCoroutine(Shoot());
            }
        }
        if (timer >= weapon.timeBetweenBullets * weapon.aimLineDisplayTime && weapon.weaponType != WeaponType.KNIFE)
        {
            aimLine.enabled = false;
        }
    }

    IEnumerator Shoot()
    {
        if (timer >= weapon.timeBetweenBullets && reloadTimer >= weapon.timeBetweenReload && weapon.currentBulletsInMag > 0)
        {
            timer = 0f;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            AudioSource.PlayClipAtPoint(weaponConfig.fireSound, transform.position);

            animator.SetTrigger(GetWeaponShootTrigger());

            shootParticle.Play();
            aimLine.enabled = true;
            aimLine.SetPosition(0, transform.position);

            if (Physics.Raycast(ray, out hit, weapon.shootRange))
            {
                EnemyStats enemy = hit.collider.GetComponent<EnemyStats>();
                if (enemy != null)
                {
                    enemy.ApplyDamage(weapon.bulletDamage);
                }
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

    IEnumerator Reload()
    {
        if (weapon.currentTotalBullets > 0 && weapon.currentBulletsInMag < weapon.maxBulletsPerMag)
        {
            reloadTimer = 0f;
            AudioSource.PlayClipAtPoint(weaponConfig.reloadSound, transform.position);

            int bulletsLeftInMag = weapon.maxBulletsPerMag - weapon.currentBulletsInMag;
            if (bulletsLeftInMag > weapon.currentTotalBullets)
            {
                bulletsLeftInMag = weapon.currentTotalBullets;
            }
            weapon.currentBulletsInMag += bulletsLeftInMag;
            weapon.currentTotalBullets -= bulletsLeftInMag;

            animator.SetTrigger(GetWeaponReloadTrigger());

            //Delay updateText while reloading.
            yield return new WaitForSeconds(weapon.timeBetweenReload);
            UpdateWeaponText();
        }
    }

    public void PlayDrawSound()
    {
        AudioSource.PlayClipAtPoint(weaponConfig.drawSound, transform.position);
    }
    public void UpdateWeaponText()
    {
        weaponText.text = this.name + Environment.NewLine;
        if (weapon.weaponType != WeaponType.KNIFE)
            weaponText.text += "Ammo: " + weapon.currentBulletsInMag + "/" + weapon.currentTotalBullets;
        weaponImage.sprite = weaponConfig.weaponSprite;
    }

    private int GetWeaponShootTrigger()
    {
        return Animator.StringToHash("Shoot");
    }
    private int GetWeaponReloadTrigger()
    {
        return Animator.StringToHash("Reload");
    }
}
