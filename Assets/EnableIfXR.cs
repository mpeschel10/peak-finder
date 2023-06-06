using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnableIfXR : MonoBehaviour
{
    static bool? isXR;
    [SerializeField] Behaviour target;
    void Awake()
    {
        if (target == null)
        {
            if (IsThisJustFantasy())
            {
                Debug.Log(gameObject + " is enabled since XR is available.");
            } else {
                gameObject.SetActive(false);
                Debug.Log("Disabling " + gameObject + " since XR is not available.");
            }
        } else {
            if (IsThisJustFantasy())
            {
                Debug.Log(target + " is enabled since XR is available.");
            } else
            {
                target.enabled = false;
                Debug.Log("Disabling " + target + " since XR is not available.");
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
