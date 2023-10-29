using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloading : MonoBehaviour
{
    private float _currentReloadTime;
    private float _startedReloadTime;
    private GunData _weaponData;
    private bool _justReloaded;
    [SerializeField] private Slider _slider;

    void Start()
    {
        _currentReloadTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        _weaponData = GameObject.Find("Player").GetComponentInChildren<WeaponHolder>().GetWeaponData();

        if (_weaponData)
        {
            if (_weaponData.reloading && !_justReloaded)
            {
                _currentReloadTime = 0;
                _slider.value = 0;
                _slider.gameObject.SetActive(true);
                _justReloaded = true;
            } else if (_weaponData.reloading && _justReloaded)
            {
                _slider.maxValue = _weaponData.reloadTime;
                _slider.value = _currentReloadTime;
                _currentReloadTime += Time.deltaTime;
            } else if (!_weaponData.reloading && _justReloaded)
            {
                _slider.gameObject.SetActive(false);
                _justReloaded = false;
            }
        }
    }
}
