using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kill : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text _scoreText;

    private int _kill = 0;

    public void addKill()
    {
        _kill++;
        _scoreText.text = _kill.ToString();
    }
}
