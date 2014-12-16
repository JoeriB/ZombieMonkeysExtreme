using UnityEngine;
using System.Collections;
using System;

public class CharacterDetails : MonoBehaviour
{

    public string characterName;
    public string description;
    public float walkSpeed;
    public float crouchSpeed;
    public float runSpeed;

    public override string ToString()
    {
        return String.Format("Character Details:\n - Name: {0}\n - Description: {1}\n - Walking Speed: {2}\n - Crouching Speed: {3}\n - Running Speed: {4}",
                                characterName, description, walkSpeed, crouchSpeed, runSpeed
                            );
    }
}
