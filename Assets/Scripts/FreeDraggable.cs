using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeDraggable : MonoBehaviour, CameraSelector.Hoverable, CameraSelector.Draggable
{

    public void Hover() { gameObject.GetComponent<LayeredOutline>().AddLayer("can-drag"); }
    public void Unhover() { gameObject.GetComponent<LayeredOutline>().SubtractLayer("can-drag"); }
    public GameObject GetGameObject() { return gameObject; }

    Transform grabTransform;
    Vector3 grabOffset;
    
    Follower follower;
    void Awake()
    {
        follower = transform.parent.GetComponentInChildren<Follower>();
    }
    
    public void Grab(Transform grabTransform)
    {
        this.grabTransform = grabTransform;
        this.grabOffset = grabTransform.position - this.transform.position;
    }

    public void Ungrab()
    {
        this.grabTransform = null;
    }

    private void Update()
    {
        if (grabTransform != null)
        {
            transform.position = grabTransform.position - grabOffset;
            follower.Follow();
        }
    }
}
