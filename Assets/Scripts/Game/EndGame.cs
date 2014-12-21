using UnityEngine;
using System.Collections;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme End Game: Checks if we can end the game. If true it will show the end game screen.
 */
public class EndGame : MonoBehaviour
{

    public DoorManager endGameDoor;
    public GameObject player;

    bool canEndGame;
    Inventory inventory;

    void Start()
    {
        inventory = player.GetComponent<Inventory>();
    }

    void Update()
    {
        if (canEndGame && !endGameDoor.isDoorOpen())
        {
            if (inventory.hasAllItems())
                //TODO: Nieuw scherm met all end game statistis! gewoon zwart en rustig naar beneden laten gaan met animations?
                Debug.Log("Ending game; Show End Game Statistics");
            else
                Debug.Log("You need more items...");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Player"))
            canEndGame = true;
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag.Equals("Player"))
            canEndGame = true;
    }
}
