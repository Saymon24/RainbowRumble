using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToSelectionMenu()
    {
        LoadingManager.instance.changeNextSceneToLoad("EnemiesDebug");
        SceneManager.LoadScene("LoadingScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
