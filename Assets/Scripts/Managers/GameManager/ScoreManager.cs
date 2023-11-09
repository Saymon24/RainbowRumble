using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        GameObject.Find("UI").GetComponentInChildren<Score>().UpdateScore(score);
    }

}
