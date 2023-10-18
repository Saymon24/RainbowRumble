using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text _scoreText;

    private int _score = 0;

    public void addScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }
}
