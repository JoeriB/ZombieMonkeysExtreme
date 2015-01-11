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

    [SerializeField]
    private AudioClip failEndGame;
    [SerializeField]
    private AudioClip successEndGame;
    private bool canEndGame;
    private bool ending;
    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag(TagManager.player).GetComponent<Inventory>();
    }

    void Update()
    {
        if (canEndGame && !endGameDoor.isDoorOpen())
        {
            if (inventory.hasAllItems() && !ending)
                StartCoroutine(EndTheGame());
        }
    }

    public IEnumerator EndTheGame()
    {
        ending = true;
        GameObject player = GameObject.FindGameObjectWithTag(TagManager.player);
        Screen.lockCursor = false;

        deathScreen.SetActive(true);
        deathScreen.GetComponentInChildren<Text>().text = player.GetComponent<PlayerStats>().GetEndGameText();

        GetComponent<AudioSource>().PlayOneShot((player.GetComponent<PlayerStats>().isPlayerDead() ? failEndGame : successEndGame));

        foreach (GameObject monkey in uiMonkeys)
        {
            monkey.SetActive(monkey);
        }

        player.GetComponent<FPSPlayerMovement>().enabled = false;
        player.GetComponent<FPSMouseMovement>().enabled = false;
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

    public bool IsEnding()
    {
        return ending;
    }
}
