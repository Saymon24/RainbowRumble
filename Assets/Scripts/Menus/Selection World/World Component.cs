using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldComponent : MonoBehaviour
{
    public string nameOfScene;
    public string name;

    private void Start()
    {
        if (GameObject.Find("WorldSelection").TryGetComponent(out SelectionWorld selection))
        {
            if (selection.selectedWorld == name)
            {
                Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();

                foreach (Transform child in allChildren)
                {
                    if (child.gameObject.name == "Border")
                        child.gameObject.SetActive(true);
                }
            }
        }
    }

    private void SelectWorld()
    {
        if (GameObject.Find("WorldSelection").TryGetComponent(out SelectionWorld selection))
        {
            
        }
    }
}
