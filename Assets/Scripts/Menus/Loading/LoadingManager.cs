using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    static public LoadingManager instance;

    private string sceneName = "";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void changeNextSceneToLoad(string newSceneName)
    {
        sceneName = newSceneName;
    }

    public string getNextSceneToLoad()
    {
        return sceneName;
    }
}
