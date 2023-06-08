using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerBodyFollower : MonoBehaviour, Follower
{
    [SerializeField] GameObject rulerStart, rulerEnd;
    [SerializeField] Transform rulerTapeTransform;

    public void Follow()
    {
        
    }
    public void Update()
    {
        Vector3 startEndOffset = rulerEnd.transform.position - rulerStart.transform.position;
        float distance = startEndOffset.magnitude;
        
        float newHeight = distance / 2;
        Vector3 oldScale = rulerTapeTransform.localScale;
        rulerTapeTransform.localScale = new Vector3(oldScale.x, newHeight, oldScale.z);
        
        Vector3 newPosition = rulerStart.transform.position + startEndOffset.normalized * newHeight;
        transform.position = newPosition;

        transform.LookAt(rulerEnd.transform.position);
    }
}
