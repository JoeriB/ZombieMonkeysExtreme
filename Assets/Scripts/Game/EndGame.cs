using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme End Game: Checks if we can end the game. If true it will show the end game screen.
 */
public class EndGame : MonoBehaviour
{

    public DoorManager endGameDoor;
    public GameObject deathScreen;
    public GameObject[] uiMonkeys;
    public float endGameDelay;

    private bool canEndGame;
    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag(TagManager.player).GetComponent<Inventory>();
    }

    void Update()
    {
        if (canEndGame && !endGameDoor.isDoorOpen())
        {
            if (inventory.hasAllItems())
                StartCoroutine(EndTheGame());
        }
    }

    public IEnumerator EndTheGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag(TagManager.player);
        Screen.lockCursor = false;

        deathScreen.SetActive(true);
        deathScreen.GetComponentInChildren<Text>().text = player.GetComponent<PlayerStats>().GetEndGameText();

        foreach (GameObject monkey in uiMonkeys)
        {
            monkey.SetActive(monkey);
        }

        GetComponent<FPSMouseMovement>().enabled = false;
        GameObject.FindGameObjectWithTag(TagManager.weaponManager).SetActive(false);
        GameObject.FindGameObjectWithTag(TagManager.playerHUD).SetActive(false);
        GameObject.FindGameObjectWithTag(TagManager.ui).GetComponent<EscapeMenu>().enabled = false;
        GameObject.FindGameObjectWithTag(TagManager.uiPanel).GetComponent<Image>().color = new Color(0, 0, 0, 146);
        yield return new WaitForSeconds(endGameDelay);
        player.SetActive(false);
        Application.LoadLevel(Application.loadedLevel);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals(TagManager.safeHouse))
            canEndGame = true;
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag.Equals(TagManager.safeHouse))
            canEndGame = false;
    }
}
