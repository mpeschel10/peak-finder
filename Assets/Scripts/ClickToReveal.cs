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
    public void Click()
    {
        MakePillars parent = gameObject.transform.parent.parent.gameObject.GetComponent<MakePillars>();
        ClickToReveal[] neighbors = parent.hiddenPillars;
        #nullable enable
        ClickToReveal? leftNeighbor = index > 0 ? neighbors[index - 1] : null;
        ClickToReveal? rightNeighbor = index < neighbors.Length - 1 ? neighbors[index + 1] : null;
        leftNeighbor?.Reveal();
        this.Reveal();
        rightNeighbor?.Reveal();
        #nullable disable
    }

    public void Reveal()
    {
        gameObject.SetActive(false);
        visiblePillar.SetActive(true);
    }
}
