﻿using UnityEngine;
using System.Collections;
using System;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Weapon Manager: Handles our switching and contains our weapon list
 */
public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weaponList;
    [SerializeField]
    private int currentWeaponIndex = 0;
    [SerializeField]
    private GameObject currentWeapon;

    public void Initiate()
    {
        ActivateNextWeapon();

        WeaponCombat weaponCombat = currentWeapon.GetComponent<WeaponCombat>();
        weaponCombat.Initiate();
        weaponCombat.DrawWeapon();
        weaponCombat.UpdateWeaponText();
    }

    void Update()
    {
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

    private void SelectWeapon()
    {
        ActivateNextWeapon();

        WeaponCombat weaponCombat = currentWeapon.GetComponent<WeaponCombat>();
        weaponCombat.DrawWeapon();
        weaponCombat.UpdateWeaponText();
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
