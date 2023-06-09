using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuShowHide : MonoBehaviour
{
    public InputActionReference menuButtonReference;
    void Awake()
    {
        menuButtonReference.action.performed += ToggleShowHide;
        gameObject.SetActive(false);
    }

    void ToggleShowHide(InputAction.CallbackContext context)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
