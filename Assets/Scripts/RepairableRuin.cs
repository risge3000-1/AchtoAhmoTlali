using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RepairableRuin : MonoBehaviour
{
    public bool haveIBeenRepaired = false,
                hasNotChangedColorsYet = true,
                isPlayerNearMe = false;

    public string historyWhenNotRepaired;
    public string historyWhenRepaired;

    Light RuinLight;

    //Collider TriggerZone;

    Color newColorOfLight;

    private void Awake()
    {
        RuinLight = gameObject.GetComponentInChildren<Light>();
    }

    public void Update()
    {
        if (hasNotChangedColorsYet)
        {
            if (haveIBeenRepaired)
                newColorOfLight = Color.red;
            else
                newColorOfLight = Color.green;

            hasNotChangedColorsYet = false;
            
            RuinLight.color = newColorOfLight;
        }

        if (isPlayerNearMe)
        {
            if (RuinLight.intensity <= 5)
            {
                RuinLight.intensity += Time.deltaTime * 4;
            }  
        }
        else
        {
            RuinLight.intensity -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            isPlayerNearMe = true;
            if (RuinLight.intensity < 0)
            {
                RuinLight.intensity = 0;
            }
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
            isPlayerNearMe = false;
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

    public string ExportStoryText()
    {
        if (haveIBeenRepaired)
        {
            Debug.Log("I read that i need to return the repaired text wich is \"" + historyWhenRepaired + "\"");
            return historyWhenRepaired;
        }   
        else
        {
            Debug.Log("I read that i need to return the incomplete text wich is \"" + historyWhenNotRepaired + "\"");
            return historyWhenNotRepaired;
        }
            
    }
}
