using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum upgradeType
{
    DAMAGE          = 0,
    FIRE_RATE       = 1,
    MAGAZINE_SIZE   = 2,
}

[System.Serializable]
public class WeaponsUpgrades
{
    private int _maxLevel;
    private float[] _upgradePerLevel;

    public int _level;
    public string _upgradeName;
    public upgradeType _upgradeType;
    public float _upgradeValue;

    public WeaponsUpgrades(upgradeType type, string upgradeName, int level, int maxLevel, float[] upgradePerLevel)
    {
        _upgradeType = type;
        _level = level;
        _upgradeName = upgradeName;

        _maxLevel = maxLevel;
        _upgradePerLevel = upgradePerLevel;
        _upgradeValue = upgradePerLevel[level];
    }

    public void LevelUp()
    {
        if (_level + 1 > _maxLevel)
            return;

        _level = _level + 1;
        _upgradeValue = _upgradePerLevel[_level];
    }

    public void UpdatePrivateInformations(int maxLevel, float[] upgradePerLevel)
    {
        if (maxLevel < _level)
            maxLevel = _level;

        _maxLevel = maxLevel;
        _upgradePerLevel = upgradePerLevel;
        _upgradeValue = upgradePerLevel[_level];
    }

    public void PrintInfos()
    {
        Debug.Log("Upgrade Type = " + _upgradeType);
        Debug.Log("Upgrade Level = " + _level);
        Debug.Log("Upgrade Name = " + _upgradeName);
        Debug.Log("Upgrade MaxLevel = " + _maxLevel);
        foreach (var test in _upgradePerLevel)
            Debug.Log(test);

        Debug.Log("Upgrade Value = " + _upgradeValue);
    }
}
