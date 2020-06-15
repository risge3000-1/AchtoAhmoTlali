using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public Light Flashlight;
    public float horizontalSpeed, verticalSpeed;
    float horizontalMovement = 0, 
          verticalMovement = 0;
   
    float accumulatedYRotation = 0;
    
    bool isPlayerOnADialogue = false,
        isPlayerOnPause = false;

    private void Start()
    {
        DialogueManager.PlayerIsInADialogue += PlayerIsInADialogue;
        DialogueManager.PlayerIsNotInADialogue += PlayerIsNotOnADialogue;
        PlayerUIController.PlayerIsInPause += PlayerIsOnPause;
        PlayerUIController.PlayerisNotInPause += PlayerIsNotOnPause;
    }

    void Update()
    {
        if (!isPlayerOnADialogue && !isPlayerOnPause)
        {
            horizontalMovement = horizontalSpeed * Input.GetAxis("Mouse X");
            verticalMovement = verticalSpeed * Input.GetAxis("Mouse Y");
        }
        else
        {
            horizontalMovement = 0;
            verticalMovement = 0;
        }

        transform.Rotate(0, horizontalMovement, 0);

        //keep track of how much the player has looked up or down
        accumulatedYRotation += verticalMovement;

        //if player hasn't looked too much up or too much down, allow it loo look more up or down. Else, keep the tracker within range
        if (Math.Abs(accumulatedYRotation) < 90)
        {
            FirstPersonCamera.transform.Rotate(-verticalMovement, 0, 0);
            Flashlight.transform.Rotate(-verticalMovement, 0, 0);
        }
        else
            accumulatedYRotation -= verticalMovement;
    }

    void PlayerIsInADialogue()
    {
        isPlayerOnADialogue = true;
    }
    
    void PlayerIsNotOnADialogue()
    {
        isPlayerOnADialogue = false;
    }

    private void PlayerIsOnPause()
    {
        isPlayerOnPause = true;
    }

    private void PlayerIsNotOnPause()
    {
        isPlayerOnPause = false;
    }
}

