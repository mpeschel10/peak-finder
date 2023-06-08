using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowMenuOnY : MonoBehaviour
{
    public InputActionReference menuButtonReference;
    public GameObject menu;

    void OnEnable()
    {
        menuButtonReference.action.performed += SayHello;
    }

    void SayHello(InputAction.CallbackContext context)
    {
        menu.SetActive(!menu.activeSelf);
    }
}
