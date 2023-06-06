using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToReveal : MonoBehaviour, CameraSelector.Hoverable, CameraSelector.Clickable
{
    public void Hover() { GetComponent<Outline>().AddLayer("can-click"); }
    public void Unhover() { GetComponent<Outline>().SubtractLayer("can-click"); }
    public GameObject GetGameObject() { return gameObject; }

    [SerializeField] GameObject visiblePillar;
    public void Click()
    {
        gameObject.SetActive(false);
        visiblePillar.SetActive(true);
    }
}
