using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakePillars : MonoBehaviour
{
    float[] pillarHeights = { 0.4f, 0.9f, 1f, 0.9f, 0.7f, 0.3f, 0.2f, 0.01f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f, 0.8f, 0.5f};
    public ClickToReveal[] hiddenPillars;
    public float pillarScale = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject canonPillar = transform.GetChild(0).gameObject;
        canonPillar.SetActive(false);
        Vector3 ft = canonPillar.transform.position;
        Quaternion fr = canonPillar.transform.rotation;
        Vector3 pillarOffset = new Vector3(0, 0, -1);

        hiddenPillars = new ClickToReveal[pillarHeights.Length];
        for (int i = 0; i < pillarHeights.Length; i++)
        {
            float height = pillarHeights[i];
            GameObject newObject = Object.Instantiate(canonPillar, ft + pillarOffset * i, fr, transform);
            
            TMP_Text text = newObject.GetComponentInChildren<TMP_Text>();
            text.text = i.ToString();

            ClickToReveal hiddenPillar = newObject.GetComponentInChildren<ClickToReveal>();
            hiddenPillars[i] = hiddenPillar;
            hiddenPillar.index = i;
            
            PillarVisibleFlag visiblePillar = newObject.GetComponentInChildren<PillarVisibleFlag>(true);
            Vector3 s = visiblePillar.transform.localScale;
            visiblePillar.transform.localScale = new Vector3(s.x, height, s.z);
            visiblePillar.transform.localPosition = new Vector3(0.5f, height / 2, 0.5f);
            
            newObject.SetActive(true);
        }
    }
}
