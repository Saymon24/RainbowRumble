using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{

    [SerializeField] public float mouseSensitivityX = 100f;
    [SerializeField] public float mouseSensitivityY = 100f;
    [SerializeField] Transform playerBody;

    [Header("Inputs")]
    [SerializeField] private InputActionReference Xaxis;
    [SerializeField] private InputActionReference Yaxis;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Xaxis.action.ReadValue<float>() * mouseSensitivityX * Time.deltaTime;
        float mouseY = Yaxis.action.ReadValue<float>() * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
