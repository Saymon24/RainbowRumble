using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTest : MonoBehaviour
{
    private bool isLoading = false;
    [SerializeField] private float waitDuration = 0.5f;
    private float startTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime > waitDuration && !isLoading) {
            isLoading = true;
            GetComponent<LoadingScene>().LoadScene(1);
        }
    }
}
