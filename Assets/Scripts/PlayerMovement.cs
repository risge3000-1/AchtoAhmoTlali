using System;
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

    public static bool aGameItemGotPickedUp = false, aRuinHasBeenDestroyed = false, aRuinGotRepaired = false;

    public GameObject InteractionMenu;

    /*public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;*/
    Vector3 velocity;
    bool isGrounded;

    public delegate void PositionEvents();
    public static event PositionEvents IAmInABrokenRuin, IamInAFixedRuin, IAmNearAMaterial, IAmNotInARuin, IAmNotNearAMaterial; 


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
            aRuinGotRepaired = false;
            aRuinHasBeenDestroyed = false;
        }
        if (aGameItemGotPickedUp)
        {
            IAmNotNearAMaterial();
            aGameItemGotPickedUp = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //if I'm colliding with a ruin and this ruin has NOT ben fixed
        if (collision.gameObject.GetComponent<RepairableRuin>() != null && collision.gameObject.GetComponent<RepairableRuin>().haveIBeenRepaired == false)
        {
            
            IAmInABrokenRuin();

        }
        else if (collision.gameObject.GetComponent<RepairingMaterialsScript>() != null)
        {
            IAmNearAMaterial();
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
    }
}
