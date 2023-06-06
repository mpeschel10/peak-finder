using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DisableIfXR : MonoBehaviour
{
    static bool? isXR;
    [SerializeField] Behaviour target;
    void Awake()
    {
        if (target == null)
        {
            if (IsThisJustFantasy())
            {
                gameObject.SetActive(false);
                Debug.Log("Disabling " + gameObject + " since XR is available.");
            } else {
                Debug.Log(gameObject + " is enabled since no since XR is available.");
            }
        } else {
            if (IsThisJustFantasy())
            {
                target.enabled = false;
                Debug.Log("Disabling " + target + " since XR is available.");
            } else
            {
                Debug.Log(target + " is enabled since no since XR is available.");
            }
        }
    }

    public static bool IsThisTheRealWorld()
    {
        if (isXR == null)
        {
            isXR = true;
            List<XRDisplaySubsystem> xrDisplaySubsystems = new List<XRDisplaySubsystem>();
            SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
            foreach (var xrDisplay in xrDisplaySubsystems)
            {
                if (xrDisplay.running)
                {
                    isXR = false;
                    break;
                }
            }
        }
        return (bool) isXR;
    }
    public static bool IsThisJustFantasy() { return !IsThisTheRealWorld(); }
}
