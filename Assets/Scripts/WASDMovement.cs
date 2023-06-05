using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.TMP_Text;

public class WASDMovement : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float speed = 8;
    [SerializeField] float acceleration = 1.1f;
    Vector3 movementPlayerWants;
    [SerializeField] float lookSpeedX = 0.17f, lookSpeedY = 0.12f;
    public void OnMove(InputAction.CallbackContext context)
    {
        movementPlayerWants = context.ReadValue<Vector3>();
    }

    Vector2 clickLocation, lookPlayerWants;
    public void OnLook(InputAction.CallbackContext context)
    {
        InputActionPhase phase = context.phase;
        if (phase == InputActionPhase.Started)
        {
            clickLocation = context.action.ReadValue<Vector2>();
        } else if (phase == InputActionPhase.Performed)
        {
            lookPlayerWants = context.action.ReadValue<Vector2>() - clickLocation;
        } else if (phase == InputActionPhase.Canceled)
        {
            lookPlayerWants = Vector2.zero;
        }
    }
    
    float currentSpeed = 0;
    // Update is called once per frame
    void Update()
    {
        if (movementPlayerWants != Vector3.zero)
        {
            if (currentSpeed < 1)
                currentSpeed = 1;
            currentSpeed *= (float) System.Math.Pow(acceleration, Time.deltaTime);
            
            Vector3 movementPlayerGets = (cameraTransform.right   * movementPlayerWants.x +
                                        cameraTransform.up      * movementPlayerWants.y +
                                        cameraTransform.forward * movementPlayerWants.z);
            movementPlayerGets *= Time.deltaTime * speed * currentSpeed;
            transform.Translate(movementPlayerGets, Space.World);
        } else {
            currentSpeed = 0;
        }
        if (lookPlayerWants != Vector2.zero)
        {
            Quaternion oldRotation = transform.rotation;
            float xRotation = lookPlayerWants.y * -lookSpeedY;
            float yRotation = lookPlayerWants.x * lookSpeedX;
            transform.rotation = Quaternion.Euler(oldRotation.x + xRotation, oldRotation.y + yRotation, oldRotation.z);
        }
    }
}
