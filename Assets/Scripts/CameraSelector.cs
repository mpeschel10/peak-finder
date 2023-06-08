using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraSelector : MonoBehaviour
{
    public interface Hoverable {
        public void Hover(); public void Unhover();
        public GameObject GetGameObject(); // Interfaces cannot expose instance fields...
    }

    public interface Draggable {
        public void Grab(Transform transform); public void Ungrab();
    }

    public interface Clickable {
        public void Click();
    }
    
    [SerializeField] float selectionRange = 50f;
    [SerializeField] LayerMask selectableMask = 64;
    
    private Transform grabTransform;
    // We set the parent of grabTransform to be this.transform,
    //  so it follows the camera at a constant distance.
    void Start()
    {
        GameObject grabGameObject = new GameObject();
        grabTransform = grabGameObject.transform;
        grabTransform.SetParent(this.transform);
    }

    void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, selectionRange, selectableMask);
        
        DoOutlines(hitInfo);
        DoClicks(hitInfo);
        DoDrags(hitInfo);
    }

    Hoverable hoverable;
    void DoOutlines(RaycastHit hitInfo)
    {
        if ((hoverable == null && hitInfo.collider == null) ||
            (hitInfo.collider != null && hoverable != null && hitInfo.collider.gameObject == hoverable.GetGameObject()))
            return; // Nothing has changed, so don't flip-flop the outline.

        if (hoverable != null)
        {
            hoverable.Unhover();
            hoverable = null;
        }

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out hoverable))
            {
                hoverable.Hover();
            } else  {
                Debug.LogError("gameObject " + hitInfo.collider.gameObject + " on selectable layer has no Hoverable");
            }
        }
    }

    void DoClicks(RaycastHit hitInfo)
    {
        if (hitInfo.collider != null && Input.GetMouseButtonDown(0))
        {
            GameObject gameObject = hitInfo.collider.gameObject;
            if (gameObject.TryGetComponent(out Clickable clickable))
            {
                clickable.Click();
            } else {
                return;
            }
        }
    }

    Draggable dragging;
    void DoDrags(RaycastHit hitInfo)
    {
        if (hitInfo.collider != null && Input.GetMouseButtonDown(0))
        {
            if (dragging != null) // We missed a GetMouseButtonUp() somewhere; normalize.
            {
                dragging.Ungrab();
                dragging = null;
            }
            dragging = hitInfo.collider.gameObject.GetComponentInParent<Draggable>();
            if (dragging != null)
            {
                Debug.Log("Grabbed");
                // grabTransform is child of this.transform.
                // Fix the grab relative to the camera.
                grabTransform.position = hitInfo.point;
                dragging.Grab(grabTransform);
            }
        }
        if (dragging != null && Input.GetMouseButtonUp(0))
        {
            dragging.Ungrab();
            dragging = null;
        }
    }
}
