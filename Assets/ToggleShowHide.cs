using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleShowHide : MonoBehaviour
{
    public Toggle toggle;
    void Awake()
    {
        if (toggle == null)
        {
            toggle = gameObject.GetComponent<Toggle>();
        }
        toggle.onValueChanged.AddListener(delegate { ShowHide(toggle); });
    }

    GameObject ruler;
    void ShowHide(Toggle change)
    {
        if (ruler == null)
            ruler = GameObject.FindGameObjectWithTag("GameController").GetComponent<Tags>().ruler;
        ruler.SetActive(change.isOn);
    }
}
