using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CharacterMenu : MonoBehaviour
{

    public GameObject[] objects;

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
            Debug.Log("Starting game..");
            foreach (GameObject activate in objects)
            {
                activate.SetActive(true);
                Debug.Log(activate.name);
                if (activate.tag == "Player")
                {
                    activate.GetComponentInChildren<WeaponManager>().Initiate();
                    activate.GetComponent<Inventory>().Initiate();
                }
            }
            GameObject.FindGameObjectWithTag("CharacterSelect").SetActive(false);
        }
    }
}
