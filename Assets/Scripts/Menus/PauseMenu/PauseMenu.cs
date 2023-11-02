using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputActionReference pauseMenu;
    [SerializeField] GameObject UI;

    private bool isOpen = false;

    public void Update()
    {
        if (pauseMenu.action.WasPressedThisFrame())
        {
            if (isOpen)
            {
                ReturnToGame();
                isOpen = false;
            } else
            {
                isOpen = true;
                Time.timeScale = 0.0f;
                UI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void ReturnToMainMenu()
    {
        GameObject optionsManag = GameObject.Find("OptionsManager");

        if (optionsManag)
            optionsManag.GetComponent<OptionsManager>().UpdateOptions();

        Time.timeScale = 1.0f;
        LoadingManager.instance.changeNextSceneToLoad("MainMenu");
        SceneManager.LoadScene("LoadingScene");
    }

    public void ReturnToGame()
    {
        GameObject optionsManag = GameObject.Find("OptionsManager");

        Time.timeScale = 1.0f;
        isOpen = false;
        UI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (optionsManag)
            optionsManag.GetComponent<OptionsManager>().UpdateOptions();
    }
}
