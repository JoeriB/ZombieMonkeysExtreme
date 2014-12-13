using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{

    public DoorManager endGameDoor;
    public GameObject player;

    bool canEndGame;
    Inventory inventory;
    // Use this for initialization
    void Start()
    {
        inventory = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canEndGame && !endGameDoor.isDoorOpen())
        {
            if (inventory.hasAllItems())
                Debug.Log("Ending game; Show End Game Statistics");
            else
                Debug.Log("You need more items...");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name.Equals("Player"))
        {
            canEndGame = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.name.Equals("Player"))
        {
            canEndGame = true;
        }
    }
}
