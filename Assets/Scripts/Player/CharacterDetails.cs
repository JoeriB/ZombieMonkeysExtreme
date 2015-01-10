using UnityEngine;
using System.Collections;
using System;
/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Character Details: This contains detailled information about every character in our game.
 */
public class CharacterDetails : MonoBehaviour
{

    [SerializeField]
    private string characterName;
    [SerializeField]
    private string description;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float crouchSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private PlayerAbilities ability;

    public string GetName()
    {
        return characterName;
    }

    public float GetWalkSpeed()
    {
        return walkSpeed;
    }

    public float GetCrouchSpeed()
    {
        return crouchSpeed;
    }

    public float GetRunSpeed()
    {
        return runSpeed;
    }

    public PlayerAbilities GetAbility()
    {
        return ability;
    }

    public override string ToString()
    {
        return String.Format("Character Details:\n - Name: {0}\n - Description: {1}\n - Walking Speed: {2}\n - Crouching Speed: {3}\n - Running Speed: {4}\n - Special Ability: {5}",
                                characterName, description, walkSpeed, crouchSpeed, runSpeed, ability
                            );
    }
}
