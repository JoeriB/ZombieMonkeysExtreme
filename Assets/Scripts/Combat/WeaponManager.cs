using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [System.Serializable]
    public class Switch
    {
        public bool canSwitch = false;
        public float timeBetweenSwitch = 2f;
        public float switchTimer = 0f;
    }
    //public Animator animator;
    public Switch switchConfig;
    public GameObject[] weaponList;
    public int currentWeaponIndex = 0;
    public GameObject currentWeapon;

    void Start()
    {
        ActivateNextWeapon();
        //animator.SetTrigger("M4A1@Switch");
    }

    // Update is called once per frame
    void Update()
    {
        switchConfig.switchTimer += Time.deltaTime;
        if (!switchConfig.canSwitch && switchConfig.switchTimer >= switchConfig.timeBetweenSwitch)
        {
            switchConfig.canSwitch = true;
        }
        if (switchConfig.canSwitch)
        {
            switchConfig.switchTimer = 0f;
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                currentWeaponIndex = ((currentWeaponIndex + 1) < weaponList.Length) ? (currentWeaponIndex + 1) : 0;
                SelectWeapon();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                currentWeaponIndex = ((currentWeaponIndex - 1) >= 0) ? (currentWeaponIndex - 1) : (weaponList.Length - 1);
                SelectWeapon();
            }
            if (currentWeaponIndex == weaponList.Length + 1)
            {
                currentWeaponIndex = 0;
            }
            if (currentWeaponIndex == -1)
            {
                currentWeaponIndex = 0;
            }
        }
    }

    private void SelectWeapon()
    {
        ActivateNextWeapon();

        WeaponCombat weaponCombat = currentWeapon.GetComponent<WeaponCombat>();
        weaponCombat.PlayDrawSound();
        weaponCombat.UpdateWeaponText();

        //if (weaponCombat.weapon.weaponType.Equals(WeaponType.PISTOL))
        //{
        //    //Animator set pistol idle state
        //}
        //if (weaponCombat.weapon.weaponType.Equals(WeaponType.KNIFE))
        //{
        //    animator.SetBool("IsKnife", true);
        //    animator.SetTrigger("Knife@Switch");
        //}
        //if (weaponCombat.weapon.weaponType.Equals(WeaponType.MACHINE_GUN))
        //{
        //    animator.SetBool("IsKnife", false);
        //    animator.SetTrigger("M4A1@Switch");
        //}
        switchConfig.canSwitch = false;
    }

    private void ActivateNextWeapon()
    {
        //Deactive all weapons.
        foreach (GameObject weapon in weaponList)
        {
            if (weapon.renderer != null)
                weapon.renderer.enabled = false;
            weapon.SetActive(false);
        }
        //Change current weapon to the next and activate it
        currentWeapon = weaponList[currentWeaponIndex];
        if (currentWeapon.renderer != null)
        {
            currentWeapon.renderer.enabled = true;
        }
        currentWeapon.SetActive(true);
    }
}
