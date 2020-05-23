using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public float horizontalSpeed, verticalSpeed;
    float horizontalMovement, verticalMovement;
    float accumulatedYRotation = 0;

    void Update()
    {
        horizontalMovement = horizontalSpeed * Input.GetAxis("Mouse X");
        verticalMovement = verticalSpeed * Input.GetAxis("Mouse Y");

        transform.Rotate(0, horizontalMovement, 0);

        //keep track of how much the player has looked up or down
        accumulatedYRotation += verticalMovement;

        //if player hasn't looked too much up or too much down, allow it loo look more up or down. Else, keep the tracker within range
        if (Math.Abs(accumulatedYRotation) < 90)
            FirstPersonCamera.transform.Rotate(-verticalMovement, 0, 0);
        else
            accumulatedYRotation -= verticalMovement;
    }
}
