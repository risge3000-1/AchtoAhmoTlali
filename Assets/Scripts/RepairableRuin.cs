using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]

public class RepairableRuin : MonoBehaviour
{
    public bool haveIBeenRepaired = false, 
                hasNotChangedColorsYet = true;

    //Collider TriggerZone;

    MeshRenderer mRenderer;

    private void Awake()
    {
        mRenderer = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        if (hasNotChangedColorsYet)
        {
            if (haveIBeenRepaired)
                mRenderer.material.color = Color.green;
            else
                mRenderer.material.color = Color.red;

            hasNotChangedColorsYet = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if I'm colliding with the player AND I haven't been repaired
        if (other.gameObject.GetComponent<PlayerMovement>() != null && !haveIBeenRepaired)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RepairRuin();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                DestroyRuin();
            }
        }
    }

    public void RepairRuin()
    {
        haveIBeenRepaired = true;
        hasNotChangedColorsYet = true;
        PlayerMovement.aRuinGotRepaired = true;
    }

    public void DestroyRuin()
    {
        PlayerMovement.aRuinHasBeenDestroyed = true;
        gameObject.SetActive(false);   
    }
}
