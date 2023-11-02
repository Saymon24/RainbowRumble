using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyBinding : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private InputActionAsset actionMap;
    [SerializeField] private InputActionReference reference;
    
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    private InputAction action;
    private string actionPlayerPrefKey = "rebinds";

    private void Start()
    {
        action = reference.action;
        UpdateUI(reference.action.GetBindingDisplayString());
    }

    public void StartInteractiveOperation()
    {
        action.Disable();
        actionMap["Pause"].Disable();
        rebindOperation = action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .WithControlsExcluding("<Gamepad>/Start")
            .WithControlsExcluding("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f)
            .OnCancel(operation => RebindCancel())
            .OnComplete(operation => RebindComplete());

        rebindOperation.Start();
        button.GetComponentInChildren<TMP_Text>().SetText("Waiting...");
    }

    private void UpdateUI(string key)
    {
        button.GetComponentInChildren<TMP_Text>().SetText(key);
    }

    private void RebindCancel()
    {
        rebindOperation.Dispose();

        action.Enable();
        actionMap["Pause"].Enable();

        UpdateUI(action.GetBindingDisplayString());
    }

    private void RebindComplete()
    {
        rebindOperation.Dispose();

        reference.Set(action);

        string rebindingJson = actionMap.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(actionPlayerPrefKey, rebindingJson);
        PlayerPrefs.Save();

        action.Enable();
        actionMap["Pause"].Enable();

        UpdateUI(action.GetBindingDisplayString());
    }
}
