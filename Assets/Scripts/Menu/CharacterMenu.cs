using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Character Menu: Pick your character for game. This will also start up a new game.
 */
public class CharacterMenu : MonoBehaviour
{

    public GameObject[] needToBeActivatedOnStart;

    private GameObject characterDetails;
    private GameObject selectedCharacter;

    public void InitiateSelectCharacter()
    {
        characterDetails = GameObject.FindGameObjectWithTag(TagManager.characterDetails);
    }
    public void SelectCharacter(GameObject character)
    {
        selectedCharacter = character;
        characterDetails.GetComponentInChildren<Text>().text = selectedCharacter.GetComponent<CharacterDetails>().ToString();
    }

    public void StartGame()
    {
        if (selectedCharacter != null)
        {
            foreach (GameObject activationObject in needToBeActivatedOnStart)
            {
                activationObject.SetActive(true);
                Debug.Log(activationObject.name);
                if (activationObject.tag.Equals(TagManager.player))
                {
                    activationObject.GetComponent<FPSPlayerMovement>().ApplyCharacterDetails(selectedCharacter.GetComponent<CharacterDetails>());
                    activationObject.GetComponentInChildren<WeaponManager>().Initiate();
                    activationObject.GetComponent<Inventory>().Initiate();
                }
            }
            GameObject.FindGameObjectWithTag(TagManager.characterSelect).SetActive(false);
            //Only active our escape menu when we are in game..
            EscapeMenu escMenu = GetComponent<EscapeMenu>();
            escMenu.enabled = true;
            escMenu.Instantiate();


            GameObject[] uiMonkeyList = GameObject.FindGameObjectsWithTag(TagManager.uiMonkeys);
            foreach (GameObject monkey in uiMonkeyList)
            {
                monkey.SetActive(false);
            }
            GameObject.FindGameObjectWithTag(TagManager.escapeMenu).SetActive(false);
            GameObject.FindGameObjectWithTag(TagManager.uiPanel).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }
}
