using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePillars : MonoBehaviour
{
    float[] pillarHeights = { 0.01f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f};
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject firstPillar = transform.GetChild(0).gameObject;
        Vector3 ft = firstPillar.transform.position;
        Quaternion fr = firstPillar.transform.rotation;
        Vector3 pillarOffset = new Vector3(1, 0, 0);
        for (int i = 0; i < pillarHeights.Length; i++)
        {
            float height = pillarHeights[i];
            GameObject newObject = Object.Instantiate(firstPillar, ft + pillarOffset * i, fr, transform);
            PillarVisibleFlag visiblePillar = newObject.GetComponentInChildren<PillarVisibleFlag>(true);
            
            Vector3 s = visiblePillar.transform.localScale;
            visiblePillar.transform.localScale = new Vector3(s.x, height, s.z);
            
            visiblePillar.transform.localPosition = new Vector3(0.5f, height / 2, 0.5f);
            newObject.SetActive(true);
        }
    }
}
