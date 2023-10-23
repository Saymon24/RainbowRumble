using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text _ammunitionText;

    private float _ammunition = 0;
    private GunData _gunData;

    private void Start()
    {
        _gunData = GameObject.Find("Player").GetComponentInChildren<WeaponHolder>().GetWeaponData();
    }

    private void Update()
    {
        updateAmmunition(_gunData.currentAmmo);
        Debug.Log(_gunData.currentAmmo);
    }

    private void updateAmmunition(float ammunition)
    {
        _ammunition = ammunition;
        _ammunitionText.text = _ammunition.ToString();
    }
}
