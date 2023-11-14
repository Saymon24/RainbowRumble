using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    [Header("Texts")]
    [SerializeField] private TMP_Text ScoreTxt;
    [SerializeField] private TMP_Text TCTxt;
    [SerializeField] private TMP_Text RCTxt;

    private ScoreManager Score;
    private int TC;
    private int RC;
    private List<Transform> children;

    private void Start()
    {
        Score = GameObject.Find("GameManager").GetComponent<ScoreManager>();

        children = new List<Transform>();

        foreach (Transform t in transform)
        {
            children.Add(t);
            t.gameObject.SetActive(false); 
        }
    }

    public void DeathScreen()
    {
        Time.timeScale = 0.0f;

        foreach (Transform t in children)
        {
            t.gameObject.SetActive(true);
        }

        ScoreTxt.text = "Score: " + Score.score;
        CalculateCoins(Score.score);

        Cursor.lockState = CursorLockMode.None;

    }

    private void CalculateCoins(int score)
    {
        TC = score / 10_000;
        TCTxt.text = "TC: " + TC.ToString();

        if (score >= 1_000_000)
        {
            if (Random.Range(0, 100) == 0)
            {
                RC = Random.Range(1, 5);
            } else
            {
                RC = 0;
            }
        }
        RCTxt.text = "RC: " + RC.ToString();
    }

    public void GoBackToMainMenu()
    {
        LoadingManager.instance.changeNextSceneToLoad("MainMenu");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LoadingScene");
    }

    public void TryAgain()
    {
        LoadingManager.instance.changeNextSceneToLoad("Terrain");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LoadingScene");
    }
}
