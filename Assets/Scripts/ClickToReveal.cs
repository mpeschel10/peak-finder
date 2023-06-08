using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToReveal : MonoBehaviour, CameraSelector.Hoverable, CameraSelector.Clickable
{
    public void Hover() { GetComponent<Outline>().AddLayer("can-click"); }
    public void Unhover() { GetComponent<Outline>().SubtractLayer("can-click"); }
    public GameObject GetGameObject() { return gameObject; }

    [SerializeField] GameObject visiblePillar;
    public int index;
    public static bool revealPairs = false;
    public void Click()
    {
        MakePillars parent = transform.parent.parent.gameObject.GetComponent<MakePillars>();
        ClickToReveal[] neighbors = parent.hiddenPillars;
        this.Reveal();
        
        if (revealPairs)
        {
            if (index > 0 && neighbors[index - 1].gameObject.activeSelf)
                neighbors[index - 1].Reveal();
            else if (index + 1 < neighbors.Length)
                neighbors[index + 1].Reveal();
        }
    }

    public void Reveal()
    {
        gameObject.SetActive(false);
        visiblePillar.SetActive(true);
    }
}
