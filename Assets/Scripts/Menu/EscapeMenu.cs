using UnityEngine;
using System.Collections;

public class EscapeMenu : MonoBehaviour
{

    private bool pause = false;
    public GameObject pauseMenu;
    public GameObject playerHUD;
    public GameObject player;

    // Update is called once per frame
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

        //Zet tijd op 0... + menu activeren en player HUD afzetten + cursor aanzetten
        Time.timeScale = (pause) ? 0.0f : 1.0f;
        pauseMenu.SetActive(pause);
        playerHUD.SetActive(!pause);
        Screen.lockCursor = !pause;

        //Scripts/AudioListener in/uitschakelen
        player.GetComponent<MouseLook>().enabled = !pause;
        Camera.main.GetComponent<AudioListener>().enabled = !pause;
        player.GetComponentInChildren<WeaponCombat>().enabled = !pause;
    }
}
