using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakePillars : MonoBehaviour
{
    float[] pillarHeights;
    int pillarCount = 20;
    public ClickToReveal[] hiddenPillars;
    public float pillarScale = 4;
    [SerializeField] GameObject canonicalPillar;
    [SerializeField] TMP_Text costText;

    // static float MIN_HEIGHT = 0.01f;
    // static float MAX_HEIGHT = 1f;
    
    int cost;
    void Start()
    {
        canonicalPillar.SetActive(false);
        Vector3 ft = canonicalPillar.transform.position;
        Quaternion fr = canonicalPillar.transform.rotation;
        Vector3 pillarOffset = new Vector3(0, 0, -1);

        cost = 0;
        UpdateCost();
        
        pillarHeights = new float[pillarCount + 4];
        pillarHeights[0] = -1;
        pillarHeights[1] = 0;
        pillarHeights[pillarHeights.Length - 2] = 0;
        pillarHeights[pillarHeights.Length - 1] = -1;
        
        hiddenPillars = new ClickToReveal[pillarHeights.Length - 4];
        for (int i = 0; i < hiddenPillars.Length; i++)
        {
            pillarHeights[i + 2] = float.NaN;
            GameObject newObject = Object.Instantiate(canonicalPillar, ft + pillarOffset * i, fr, transform);
            
            TMP_Text text = newObject.GetComponentInChildren<TMP_Text>();
            text.text = i.ToString();

            ClickToReveal hiddenPillar = newObject.GetComponentInChildren<ClickToReveal>();
            hiddenPillars[i] = hiddenPillar;
            hiddenPillar.index = i;
            
            newObject.SetActive(true);
        }
    }

    void UpdateCost() { costText.text = "Cost: " + cost; }

    int SeekCollapsed(int index, int direction)
    {
        while (pillarHeights[index].Equals(float.NaN))
        {
            index += direction;
        }
        return index;
    }

    int SeekCollapsedBelow(int index, int direction, float peak)
    {
        while (!(pillarHeights[index] < peak))
        {
            index += direction;
        }
        return index;
    }

    public void Collapse(int index)
    {
        index += 2;

        float height = -1f;
        if (cost == 0)
        {
            height = 0.5f;
        } else {
            int leftRegionBoundary = SeekCollapsed(index, -1);
            int rightRegionBoundary = SeekCollapsed(index, 1);
            
            float left = pillarHeights[leftRegionBoundary];
            float right = pillarHeights[rightRegionBoundary];
            int leftEdgeBoundary = SeekCollapsedBelow(leftRegionBoundary, -1, left);
            int rightEdgeBoundary = SeekCollapsedBelow(rightRegionBoundary, 1, right);
            Debug.Log($"{leftEdgeBoundary - 2}, {leftRegionBoundary - 2}, {rightRegionBoundary - 2}, {rightEdgeBoundary - 2}");

            int spanIfAbove = rightRegionBoundary - leftRegionBoundary - 1;
            int spanIfBetween = right > left ? rightEdgeBoundary - index - 1 : index - leftEdgeBoundary - 1;
            //int spanIfBelow; // This is always <= spanIfBetween, but might add flavor if I have time to implement it.

            if (spanIfAbove > spanIfBetween)
            {
                Debug.Log("Selecting above");
                // height must right < height.
                height = System.Math.Max(right, left) + 0.1f;
            } else {
                Debug.Log("Selecting between");
                height = (right + left) / 2;
            }

        }

        pillarHeights[index] = height;
        index -= 2;
        
        ClickToReveal hiddenPillar = hiddenPillars[index];
        GameObject visiblePillar = hiddenPillar.visiblePillar;
        Vector3 s = visiblePillar.transform.localScale;
        visiblePillar.transform.localScale = new Vector3(s.x, height * pillarScale, s.z);
        visiblePillar.transform.localPosition = new Vector3(0.5f, height * pillarScale / 2, 0.5f);
        hiddenPillar.Reveal();
    }

    public void Click(int index)
    {
        Collapse(index);
        if (ClickToReveal.revealPairs)
        {
            if (index > 0 && hiddenPillars[index - 1].gameObject.activeSelf)
                Collapse(index - 1);
            else if (index + 1 < hiddenPillars.Length)
                Collapse(index + 1);
        }
    }

    public void Increment()
    {
        cost++;
        UpdateCost();
    }
}
