using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSway : MonoBehaviour
{

    [Header("Sway Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float sensitivityMultiplier;

    [Header("Inputs")]
    [SerializeField] private InputActionReference Xaxis;
    [SerializeField] private InputActionReference Yaxis;

    private void Update()
    {
        // get mouse input
        float mouseX = Xaxis.action.ReadValue<float>() * sensitivityMultiplier;
        float mouseY = Yaxis.action.ReadValue<float>() * sensitivityMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
    }
}