using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CharacterMenu : MonoBehaviour
{

    public GameObject[] needToBeActivatedOnStart;

    private GameObject characterDetails;
    private GameObject selectedCharacter;

    public void InitiateSelectCharacter()
    {
        characterDetails = GameObject.FindGameObjectWithTag("CharacterDetails");
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
                if (activationObject.tag == "Player")
                {
                    activationObject.GetComponent<FPSPlayerMovement>().ApplyCharacterDetails(selectedCharacter.GetComponent<CharacterDetails>());
                    activationObject.GetComponentInChildren<WeaponManager>().Initiate();
                    activationObject.GetComponent<Inventory>().Initiate();
                }
            }
            GameObject.FindGameObjectWithTag("CharacterSelect").SetActive(false);
        }
    }
}
