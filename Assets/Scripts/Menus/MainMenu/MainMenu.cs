using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        AudioManager.instance.PlayMusic("MainTheme");
    }

    public void GoToSelectionMenu()
    {
        LoadingManager.instance.changeNextSceneToLoad("AlphaLevel");
        SceneManager.LoadScene("LoadingScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
