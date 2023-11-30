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

        if (GameObject.Find("SaveManager").TryGetComponent(out ApplySave save))
        {
            save.ApplySaveOnPlayer();
        }
    }
}
