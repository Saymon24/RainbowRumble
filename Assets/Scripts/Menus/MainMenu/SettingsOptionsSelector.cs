using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOptionsSelector : MonoBehaviour
{
    [Serializable]
    class SubMenuSettings
    {
        public GameObject _settingsTab;
        public GameObject _settingsMenu;
    }

    [SerializeField] private SubMenuSettings[] menuSettings;

    private void Awake()
    {
        foreach (SubMenuSettings subMenu in menuSettings)
            subMenu._settingsMenu.SetActive(false);
        menuSettings[0]._settingsMenu.SetActive(true);
    }

    public void ChangeActiveSettingsMenu(GameObject tabClicked)
    {
        foreach (SubMenuSettings subMenu in menuSettings)
        {
            if (subMenu._settingsTab.GetInstanceID() == tabClicked.GetInstanceID())
            {
                subMenu._settingsMenu.SetActive(true);
            }
            else
            {
                subMenu._settingsMenu.SetActive(false);
            }
        }
    }
}
