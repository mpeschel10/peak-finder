using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleRevealPairs : MonoBehaviour
{
    public Toggle toggle;
    void Start()
    {
        if (toggle == null) toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            Toggle();
        });
    }

    void Toggle()
    {
        ClickToReveal.revealPairs = toggle.isOn;
    }
}
