using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ammunition : MonoBehaviour
{
    [Header("Text & Image")]
    [SerializeField] private TMP_Text _ammunitionText;
    [SerializeField] private Image _ammunitionVisual;

    private float _ammunition = 0;
    private GunData _gunData;

    private void Start()
    {
    }

    private void Update()
    {
        _gunData = GameObject.Find("Player").GetComponentInChildren<WeaponHolder>().GetWeaponData();

        if (_gunData != null)
        {
            updateAmmunition(_gunData.currentAmmo);
        }
    }

    private void updateAmmunition(float ammunition)
    {
        _ammunition = ammunition;
        _ammunitionText.text = _ammunition.ToString();
        _ammunitionVisual.rectTransform.sizeDelta = new Vector2(_ammunitionVisual.rectTransform.sizeDelta.x, 5 * ammunition);
    }
}
