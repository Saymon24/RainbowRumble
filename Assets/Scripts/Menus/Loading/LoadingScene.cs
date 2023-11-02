using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    private bool isLoading = false;
    [SerializeField] private float waitDuration = 0.5f;
    private float startTime = 0.0f;

    void Start()
    {
        startTime = Time.time;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime > waitDuration && !isLoading)
        {
            isLoading = true;
            Debug.Log(LoadingManager.instance.getNextSceneToLoad());
            GetComponent<LoadingScene>().LoadScene(LoadingManager.instance.getNextSceneToLoad());
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
