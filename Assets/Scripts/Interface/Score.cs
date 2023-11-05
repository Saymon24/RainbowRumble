using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text _scoreText;

    public void Update()
    {
        /*if (FindAnyObjectByType<CombatManager>().isInCombat)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);*/
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
