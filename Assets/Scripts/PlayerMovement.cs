﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Light Flashlight;

    public static bool aGameItemGotPickedUp = false, aRuinHasBeenDestroyed = false, aRuinGotRepaired = false, hasInteractedWithAllRuins = false;
    public static string materialTypeToGive;

    public GameObject InteractionMenu;

    /*public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;*/
    Vector3 velocity;
    bool isGrounded;

    public delegate void PositionEvents();
    public static event PositionEvents IAmInABrokenRuin, IamInAFixedRuin, IAmNearAMaterial, IAmNotInARuin, IAmNotNearAMaterial, IAmNearThePyramid, IAmNotInThePyramid;

    public delegate void UserActionsEvents();
    public static event UserActionsEvents IrepairedARuin, IDestroyedARuin, IPickedUpSomething;

    private void Start()
    {
        PlayerScore.HasInteractedWithAllRuins += PlayerHasInteractedWithallRuins;
    }

    void Update()
    {
        /*isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }*/
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move*speed*Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (SunRotation.isItNight)
            Flashlight.intensity = 2;
        else
            Flashlight.intensity = 0;

        if (aRuinHasBeenDestroyed || aRuinGotRepaired)
        {
            IAmNotInARuin();

            if (aRuinGotRepaired)
            {
                IrepairedARuin();
                IamInAFixedRuin();
            }
            else if (aRuinHasBeenDestroyed)
            {
                Debug.Log("I read I am destroying a ruin and I'm announcing it");

                IDestroyedARuin();

                Debug.Log("x as " + transform.position.x + " y as " + transform.position.y + " z as " + transform.position.z);
                RandomGameItemCreator.GenerateRandomMaterial(transform.position.x, transform.position.y, transform.position.z);
            }

            aRuinGotRepaired = false;
            aRuinHasBeenDestroyed = false;
        }



        if (aGameItemGotPickedUp)
        {
            IAmNotNearAMaterial();
            aGameItemGotPickedUp = false;
            gameObject.GetComponent<Inventory>().GiveItem(itemType:materialTypeToGive);
            materialTypeToGive = "" ;
            //IPickedUpSomething();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //if I'm colliding with a ruin and this ruin has NOT ben fixed
        if (collision.gameObject.GetComponent<RepairableRuin>() != null )
        {

            if (collision.gameObject.GetComponent<RepairableRuin>().haveIBeenRepaired == false)
            {
                IAmInABrokenRuin();
            }
            else
            {
                IamInAFixedRuin();
            }
            
            

        }
        else if (collision.gameObject.GetComponent<RepairingMaterialsScript>() != null)
        {
            IAmNearAMaterial();
        }
        else if (collision.gameObject.GetComponent<PyramidControler>() != null)
        {            
            IAmNearThePyramid();

            GetComponent<PlayerUIController>().AlterMissingRuinsMessage();
            if (hasInteractedWithAllRuins)
            {
                QuitGame();
            }
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<RepairableRuin>() != null)
        {
            IAmNotInARuin();
        }
        else if (collision.gameObject.GetComponent<RepairingMaterialsScript>() != null)
        {
            IAmNotNearAMaterial();
        }
        else if (collision.gameObject.GetComponent<PyramidControler>() != null)
        {
            IAmNotInThePyramid();
        }
    }

    private void QuitGame()
    {
        new WaitForSeconds(3);
        Application.Quit();
    }

    private void PlayerHasInteractedWithallRuins()
    {
        hasInteractedWithAllRuins = true;
    }
}
