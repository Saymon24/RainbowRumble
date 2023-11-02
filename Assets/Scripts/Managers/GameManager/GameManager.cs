using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (!player.GetComponent<PlayerManager>().isAlive)
        {
            LoadingManager.instance.changeNextSceneToLoad("MainMenu");
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
