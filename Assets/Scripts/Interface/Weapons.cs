using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    private List<GunData> _weaponsData;
    private string _oldWeaponMainName = "";
    private string _oldWeaponSubName = "";

    [SerializeField] private Image _mainWeapon;
    [SerializeField] private Image _subWeapon;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _weaponsData = GameObject.Find("Player").GetComponentInChildren<WeaponHolder>().GetWeaponsData();

        if (HasSubChanged())
        {
            if (_oldWeaponSubName == "")
            {
                _subWeapon.sprite = Resources.Load<Sprite>(_weaponsData[1].name);
                _oldWeaponSubName = _weaponsData[1].name;
            } else
            {
                if (_oldWeaponSubName == _oldWeaponMainName)
                {
                    Sprite tmp = _mainWeapon.sprite;
                    _mainWeapon.sprite = _subWeapon.sprite;
                    _subWeapon.sprite = tmp;
                } else
                {
                    _mainWeapon.sprite = _subWeapon.sprite;
                    _subWeapon.sprite = Resources.Load<Sprite>(_weaponsData[1].name);
                    _oldWeaponSubName = _weaponsData[1].name;
                }
            }
        }
        _oldWeaponMainName = _weaponsData[0].name;
    }

    private bool HasSubChanged()
    {
        if (_weaponsData.Count > 1)
            return _weaponsData[1].name != _oldWeaponSubName;
        else return false;
    }
}
