using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Escape Menu: Pauses the game
 */
public class EscapeMenu : MonoBehaviour
{
    public GameObject[] uiMonkeys;
    private bool pause = false;

    private GameObject escapeMenu;
    private GameObject player;
    private GameObject playerHUD;

    public void Instantiate()
    {
        player = GameObject.FindGameObjectWithTag(TagManager.player);
        escapeMenu = GameObject.FindGameObjectWithTag(TagManager.escapeMenu);
        playerHUD = GameObject.FindGameObjectWithTag(TagManager.playerHUD);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void ClickButton(string message)
    {

        if (message.Equals("resume"))
        {
            PauseGame();
        }
        if (message.Equals("quit"))
        {
            Application.Quit();
        }
    }

    void PauseGame()
    {
        pause = !pause;

        //Set time to 0 + Activate menu and Deactive Player HUD etc..
        Time.timeScale = (pause) ? 0.0f : 1.0f;
        escapeMenu.SetActive(pause);
        playerHUD.SetActive(!pause);
        Screen.lockCursor = !pause;

        //Scripts/AudioListener in/uitschakelen
        player.GetComponent<FPSMouseMovement>().enabled = !pause;
        Camera.main.GetComponent<AudioListener>().enabled = !pause;
        player.GetComponentInChildren<WeaponCombat>().enabled = !pause;

        //Monkey UI
        foreach (GameObject monkey in uiMonkeys)
        {
            monkey.SetActive(pause);
        }
        //Transparency of our game in pause/normal mode 
        GameObject.FindGameObjectWithTag(TagManager.uiPanel).GetComponent<Image>().color = new Color(0, 0, 0, (pause) ? 255 : 0);
    }
}
