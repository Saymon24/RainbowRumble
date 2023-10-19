using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text _ammunitionText;

    private int _ammunition = 0;
    private WeaponHolder _weaponHolder;

    private void Start()
    {
        /*_weaponHolder = GameObject.Find("Player").GetComponentInChildren<WeaponHolder>().GetSelectedWeapon();*/
    }

    private void Update()
    {
    }

    public void updateAmmunition(int ammunition)
    {
        _ammunition = ammunition;
        _ammunitionText.text = _ammunition.ToString();
    }
}
