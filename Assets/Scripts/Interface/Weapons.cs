using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    private GunData _weaponData;
    private string _oldWeaponName = "";

    [SerializeField] private Image _mainWeapon;
    [SerializeField] private Image _subWeapon;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _weaponData = GameObject.Find("Player").GetComponentInChildren<WeaponHolder>().GetWeaponData();

        if (_weaponData)
        {
            if (_weaponData.name != _oldWeaponName && _oldWeaponName != "")
            {
                Sprite tmp = _mainWeapon.sprite;
                _mainWeapon.sprite = _subWeapon.sprite;
                _subWeapon.sprite = tmp;
            }
            _oldWeaponName = _weaponData.name;
        }
    }
}
