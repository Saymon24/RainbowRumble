using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuButtonHover : MonoBehaviour
{
    private void Update()
    {
    }
    public void OnPointerEnter( )
    {
        Debug.Log("enter");
        transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
    }

    public void OnPointerExit( )
    {
        Debug.Log("exit");
        transform.position = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
    }
}
