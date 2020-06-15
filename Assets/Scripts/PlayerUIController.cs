using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    bool isPlayerOnARuin = false,
         isPlayerNearAMaterial = false,
         isPlayerNearThePyramid = false,
         showMissingRuinsInfo = true,
         isPlayerOnAFixedRuin = false,
         haveIImportedTheRuinStory = false,
         isPlayerOnACollision = false;

    public Text OptionsText,
                MessagesText;

    public string newOptionsText,
                  newMessagesText;

    public string importedMaterialOptionsText,
                  importedRuinOptionsText,
                  importedPyramidOptionsText,
                  importedMaterialMessageText,
                  importedRuinMessageText,
                  importedPyramidMessageText;

    public bool isPlayerOnADialogue = false,
                isPlayerOnPause = false;

    public GameObject PauseMenu;


    public delegate void PauseEvents();
    public static event PauseEvents PlayerIsInPause, PlayerisNotInPause;

    Color newOptionsTextColor,
          newMessagesTextColor;

    public DialogueTrigger initalDialogue, finalDialogue;

    readonly float changeFactor = 2;

    void Start()
    {
        //subscribe functions to events
        PlayerMovement.IAmInABrokenRuin += RuinEntering;
        PlayerMovement.IamInAFixedRuin += FixedRuinEntering;
        PlayerMovement.IAmNotInARuin += RuinExiting;

        PlayerMovement.IAmNearAMaterial += BeingNearAMaterial;
        PlayerMovement.IAmNotNearAMaterial += NotBeingNearAMaterial;

        PlayerMovement.IAmNearThePyramid += BeingCloseToPyramid;
        PlayerMovement.IAmNotInThePyramid += SeparatingFromPyramid;

        PlayerMovement.IDestroyedARuin += AlterPyramidMessageText;
        PlayerMovement.IrepairedARuin += AlterPyramidMessageText;

        PlayerScore.HasInteractedWithAllRuins += DoNotShowMissingRuinsInfo;

        DialogueManager.PlayerIsInADialogue += PlayerIsInADialogue;
        DialogueManager.PlayerIsNotInADialogue += PlayerIsNotInADialogue;


        newOptionsTextColor = OptionsText.color;
        newMessagesTextColor = MessagesText.color;

        //set colors's alpha to 0
        newMessagesTextColor.a = 0;
        newOptionsTextColor.a = 0;

        initalDialogue.TriggerDialogue();

    }

    private void Update()
    {

        if (isPlayerOnACollision)
           SolidifyText(true);
        
        else
            SolidifyText(false);
            

        if (!isPlayerOnADialogue && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPlayerOnPause)
                pausePlayer(true);
            
            else
                pausePlayer(false); 
            
        }
        
        if (!isPlayerOnADialogue && !isPlayerOnPause)
            Cursor.lockState = CursorLockMode.Locked;
        
        else
            Cursor.lockState = CursorLockMode.None; 

        UpdateTexts();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<WaterCollider>() == null && other.GetComponent<BaseReader>() == null)
            isPlayerOnACollision = true;



        if (other.GetComponent<RepairableRuin>() != null)
        {
            importedRuinMessageText = other.GetComponent<RepairableRuin>().ExportStoryText();
            importedRuinOptionsText = other.GetComponent<RepairableRuin>().optionsMessageToExport;
        }
        else if (other.GetComponent<Phase2Ruins>() != null)
        {
            importedRuinMessageText = other.GetComponent<Phase2Ruins>().ExportStoryText();
            importedRuinOptionsText = other.GetComponent<Phase2Ruins>().optionsMessageToExport;
        }
        else if (other.GetComponent<RepairingMaterialsScript>() != null)
        {
            importedMaterialMessageText = "";
            importedMaterialOptionsText = other.GetComponent<RepairingMaterialsScript>().optionsMessageToExport;
        }
        else if (other.GetComponent<PyramidControler>() != null)
        {
            importedPyramidOptionsText = "";
        }

        UpdateTexts();
    }


    private void OnTriggerExit(Collider other)
    {
        isPlayerOnACollision = false;
        UpdateTexts();
    }

    void SolidifyText(bool solidify)
    {
        float newAlphaValueToAssign = Time.deltaTime * changeFactor;

        if (solidify)
        {
            if (newOptionsTextColor.a < 0)
            {
                newOptionsTextColor.a = 0;
                newMessagesTextColor.a = 0;
            }
            
            newOptionsTextColor.a += newAlphaValueToAssign;
            newMessagesTextColor.a += newAlphaValueToAssign;
        }
        else
        {
            if (newOptionsTextColor.a > 1)
            {
                newOptionsTextColor.a = 1;
                newMessagesTextColor.a = 1;
            }

            newOptionsTextColor.a -= newAlphaValueToAssign;
            newMessagesTextColor.a -= newAlphaValueToAssign;
        }

        OptionsText.color = newOptionsTextColor;
        MessagesText.color = newMessagesTextColor;

    }

    void RuinEntering()
    {
        isPlayerOnARuin = true;
        haveIImportedTheRuinStory = false;

    }

    void FixedRuinEntering()
    {
        isPlayerOnAFixedRuin = true;
        haveIImportedTheRuinStory = false;

    }

    void RuinExiting()
    {
        isPlayerOnARuin = false;
        isPlayerOnAFixedRuin = false;
        isPlayerOnACollision = false;
    }

    void BeingNearAMaterial()
    {
        isPlayerNearAMaterial = true;
    }

    void NotBeingNearAMaterial()
    {
        isPlayerNearAMaterial = false;
        isPlayerOnACollision = false;
    }

    void BeingCloseToPyramid()
    {
        isPlayerNearThePyramid = true;
    }

    void SeparatingFromPyramid()
    {
        isPlayerNearThePyramid = false;
        isPlayerOnACollision = false;
    }

    void DoNotShowMissingRuinsInfo()
    {
        showMissingRuinsInfo = false;
    }

    public void AlterPyramidMessageText()
    {
        int uninteractedPhase1Ruins = GetComponent<PlayerScore>().UninteractedPhase1Ruins();
        int missingPhase2Ruins = 5 - GetComponent<PlayerScore>().pyramidControler.MissingPhase2Ruins();

        if (GetComponent<PlayerScore>().pyramidControler.hasUserinteractedWithAllRuins)
        {
            importedPyramidMessageText = "Thank you...";
        }
        else if (GetComponent<PlayerScore>().pyramidControler.hasPhase2Begun)
        {
            importedPyramidMessageText =  missingPhase2Ruins + " ruins are still on the wrong spot...";
        }
        else
        {
            importedPyramidMessageText = "Look around, " + uninteractedPhase1Ruins + " ruins still want to be treated";
        }
    }

    public string MessagesTextPrioritizer()
    {
        Debug.Log("isPlareMaterial as" + isPlayerNearAMaterial);
        
        if (isPlayerOnARuin || isPlayerOnAFixedRuin)
            return importedRuinMessageText;
        
        else if (isPlayerNearAMaterial)
            return importedMaterialMessageText;
        
        else if (isPlayerNearThePyramid)
            return importedPyramidMessageText;
       
        else
            return newMessagesText;
    }

    public string OptionsTextsPrioritizer()
    {
        if (isPlayerOnARuin || isPlayerOnAFixedRuin)
            return importedRuinOptionsText;

        else if (isPlayerNearAMaterial)
            return importedMaterialOptionsText;

        else if (isPlayerNearThePyramid)
            return importedPyramidOptionsText;

        else
            return newOptionsText;
    }

    void UpdateTexts()
    {
        newOptionsText = OptionsTextsPrioritizer();
        newMessagesText = MessagesTextPrioritizer();

        OptionsText.text = newOptionsText;
        MessagesText.text = newMessagesText;
    }

    public void pausePlayer(bool activatePause)
    {
        if (activatePause)
        {
            Cursor.lockState = CursorLockMode.None;
            PlayerIsInPause();
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            PlayerisNotInPause();
            Time.timeScale = 1;
        }

        isPlayerOnPause = activatePause;
        PauseMenu.SetActive(activatePause);
    }

    void PlayerIsInADialogue()
    {
        isPlayerOnADialogue = true;
    }

    void PlayerIsNotInADialogue()
    {
        isPlayerOnADialogue = false;
    }
}
