using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RepairableRuin : MonoBehaviour
{
    public bool haveIBeenRepaired = false,
                hasNotChangedColorsYet = true,
                isPlayerNearMe = false,
                canIBeRepaired = false,
                amIAPhase2Ruin = false,
                amIBeingPickedUp = false;

    public bool doIHavePriority = false;

    public List<Item> itemsINeed = new List<Item>();
    public ItemDatabase itemDatabase;
    public InventoryUI inventoryUI;

    public string historyWhenNotRepaired;
    public string historyWhenRepaired;
    public string material0, material1;



    public float priorityChangeFactor = 1;

    Light RuinLight;

    Color newColorOfLight;
    new/*, because af a stupid warning, */ ParticleSystem particleSystem;

    private void Start()
    {
        RuinLight = gameObject.GetComponentInChildren<Light>();

        if (!amIAPhase2Ruin)
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();

            particleSystem.Stop();

            PyramidControler.PlayerIsNearThePyramid += ActivateParticles;
            PyramidControler.PlayerIsNotNearThePyramid += DeactivateParticles;
        }

        Item item0ToAdd = itemDatabase.GetItem(materialName:material0);
        Item item1ToAdd = itemDatabase.GetItem(materialName:material1);
        itemsINeed.Add(item0ToAdd);
        itemsINeed.Add(item1ToAdd);
    }

    public void Update()
    {
        if (hasNotChangedColorsYet)
        {
            if (haveIBeenRepaired)
                newColorOfLight = Color.red;
            else if (amIBeingPickedUp)
                newColorOfLight = Color.yellow;
            else
                newColorOfLight = Color.green;

            hasNotChangedColorsYet = false;
            
            RuinLight.color = newColorOfLight;
        }

        if (isPlayerNearMe)
        {
            if (RuinLight.intensity <= 6)
            {
                RuinLight.intensity += Time.deltaTime * 4 * priorityChangeFactor;
            }  
        }
        else
        {
            RuinLight.intensity -= Time.deltaTime * priorityChangeFactor;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        bool isItem0Aviable = false, isItem1Aviable = false ;

        string messageToExport = "";
        
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            isPlayerNearMe = true;
            if (RuinLight.intensity < 0)
            {
                RuinLight.intensity = 0;
            }

            

            for (int i = 0; i < inventoryUI.ItemsUI.Count; i++)
            {
                //if the item in the inventory slot of the player is equal to one of the required materials of the ruin, increase counter
                if (inventoryUI.ItemsUI[i].item == itemsINeed[0] && !isItem0Aviable)
                {
                    isItem0Aviable = true;
                }
                else if (inventoryUI.ItemsUI[i].item == itemsINeed[1] && !isItem1Aviable)
                {
                    isItem1Aviable = true;
                }
            }

            if (isItem0Aviable == true && isItem1Aviable == true)
            {
                canIBeRepaired = true;

               

                if (itemsINeed[0].resourceName ==  itemsINeed[1].resourceName)
                {
                    messageToExport = "F - Destroy | E - Repair with 2 " + itemsINeed[0].resourceName ;
                }
                else
                {
                    messageToExport = "F - Destroy | E - Repair with " + itemsINeed[0].resourceName + " and " + itemsINeed[1].resourceName;
                }
            }    
            else
            {
                canIBeRepaired = false;

                if (isItem0Aviable == isItem1Aviable)
                {
                    if (itemsINeed[0].resourceName == itemsINeed[1].resourceName)
                    {
                        messageToExport = "F - Destroy | You're missing 2 " + itemsINeed[0].resourceName + " items";
                    }
                    else
                    {
                        messageToExport = "F - Destroy | You're missing " + itemsINeed[0].resourceName + " and " + itemsINeed[1].resourceName;
                    }
                }
                else
                {
                    if (!isItem0Aviable)
                    {
                        messageToExport = "F - Destroy | You're missing " + itemsINeed[0].resourceName;
                    }
                    else if (!isItem1Aviable)
                    {
                        messageToExport = "F - Destroy | You're missing " + itemsINeed[1].resourceName;
                    }
                }
            }

            other.gameObject.GetComponent<PlayerUIController>().RuinOptionsText.text = messageToExport;

        }


    }

    private void OnTriggerStay(Collider other)
    {
      
        
        //if I'm colliding with the player AND I haven't been repaired
        if (other.gameObject.GetComponent<PlayerMovement>() != null && !haveIBeenRepaired)
        {
            if (!amIAPhase2Ruin || (amIAPhase2Ruin && !amIBeingPickedUp && doIHavePriority) )
            {
                if (Input.GetKeyDown(KeyCode.E) && canIBeRepaired) //decides to repair it
                {
                    haveIBeenRepaired = true;
                    hasNotChangedColorsYet = true;
                    PlayerMovement.aRuinGotRepaired = true;
                    other.gameObject.GetComponent<Inventory>().RemoveItem(itemType: itemsINeed[0].resourceName);
                    other.gameObject.GetComponent<Inventory>().RemoveItem(itemType: itemsINeed[1].resourceName);
                }
                else if (Input.GetKeyDown(KeyCode.F)) //decides to destroy it
                {
                    PlayerMovement.aRuinHasBeenDestroyed = true;
                    gameObject.SetActive(false);
                }
            }
            else if (amIBeingPickedUp)
            {
                hasNotChangedColorsYet = true;
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
            isPlayerNearMe = false;
    }

    public string ExportStoryText()
    {
        if (haveIBeenRepaired)
        {
            return historyWhenRepaired;
        }   
        else
        {
            return historyWhenNotRepaired;
        }
            
    }

    public void ActivateParticles()
    {
        if (!haveIBeenRepaired && !amIAPhase2Ruin)
            particleSystem.Play();
    }

    public void DeactivateParticles()
    {
        if (!amIAPhase2Ruin)
            particleSystem.Stop();
    }

}