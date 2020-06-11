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
         haveIImportedTheRuinStory = false;

    public enum WhatInfoHasPriority
    {
        itemOptions,
        ruinOptions,
        ruinStory,
        missingRuins,
        nothing

    }

    public Text  RuinOptionsText;
    public Image RuinOptionsBackground;
    public Text  RuinStoryText;
    public Image RuinStoryBackground;
    public Text  GameItemOptionsText;
    public Image GameItemOptionsBackground;
    public Text  MissingRuinsText;
    public Image MissingRuinsBackground;

    public string infoAmoutMissingRuins;

    Color newRuinOptionsBackgroundTint;
    Color newRuinOptionsTextTint;

    Color newRuinStoryBackgroundTint;
    Color newRuinStoryTextTint;

    Color newGameItemOptionsBackgroundTint;
    Color newGameItemOptionsTextTint;

    Color newMissingRuinsTextTint;
    Color newMissingRuinsBackgroundTint;

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

        PlayerMovement.IDestroyedARuin += AlterMissingRuinsMessage;
        PlayerMovement.IrepairedARuin += AlterMissingRuinsMessage;

        PlayerScore.HasInteractedWithAllRuins += DoNotShowMissingRuinsInfo;


        //copy current color values to avoid discrepancies
        newRuinOptionsBackgroundTint = RuinOptionsBackground.color;
        newRuinOptionsTextTint = RuinOptionsText.color;

        newGameItemOptionsBackgroundTint = GameItemOptionsBackground.color;
        newGameItemOptionsTextTint = GameItemOptionsText.color;

        newMissingRuinsBackgroundTint = MissingRuinsBackground.color;
        newMissingRuinsTextTint = MissingRuinsText.color;

        newRuinStoryBackgroundTint = RuinStoryBackground.color;
        newRuinStoryTextTint = RuinStoryText.color;

        //set colors's alpha to 0
        newRuinOptionsBackgroundTint.a = 0;
        newRuinOptionsTextTint.a = 0;

        newGameItemOptionsBackgroundTint.a = 0;
        newGameItemOptionsTextTint.a = 0;

        newMissingRuinsTextTint.a = 0;
        newMissingRuinsBackgroundTint.a = 0;

        newRuinStoryBackgroundTint.a = 0;
        newRuinStoryTextTint.a = 0;

    }

    private void Update()
    {
        //This is Only to fix the bug for when the payer interacts with te first ruin.

        //AlterMissingRuinsMessage();
        
        if (isPlayerNearAMaterial && !isPlayerOnARuin)
        {
            ColorClearnessManager(WhatInfoHasPriority.itemOptions);
        }
        else if (isPlayerNearThePyramid)
        {
            ColorClearnessManager(WhatInfoHasPriority.missingRuins);
        }
        else if (isPlayerOnAFixedRuin)
        {
            ColorClearnessManager(WhatInfoHasPriority.ruinStory);
        }
        else if (isPlayerOnARuin)
        {
            ColorClearnessManager(WhatInfoHasPriority.ruinOptions);
        }
        else
        {
            ColorClearnessManager(WhatInfoHasPriority.nothing);
        }

        if (Input.GetKey(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;


    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.GetComponent<RepairableRuin>() != null && !haveIImportedTheRuinStory)
        {
            RuinStoryText.text = collider.gameObject.GetComponent<RepairableRuin>().ExportStoryText();
            haveIImportedTheRuinStory = true;
        }
    }

    void ColorClearnessManager(WhatInfoHasPriority infoThatHasToBePrioritized)
    {
        float newAlphaValueToAssign = Time.deltaTime * changeFactor;

        newGameItemOptionsBackgroundTint.a -= newAlphaValueToAssign;
        newGameItemOptionsTextTint.a -= newAlphaValueToAssign;

        newRuinOptionsBackgroundTint.a -= newAlphaValueToAssign;
        newRuinOptionsTextTint.a -= newAlphaValueToAssign;

        newRuinStoryBackgroundTint.a -= newAlphaValueToAssign;
        newRuinStoryTextTint.a -= newAlphaValueToAssign;

        newMissingRuinsTextTint.a -= newAlphaValueToAssign;
        newMissingRuinsBackgroundTint.a -= newAlphaValueToAssign;

        switch (infoThatHasToBePrioritized)
        {
            case WhatInfoHasPriority.itemOptions:
                newGameItemOptionsBackgroundTint.a += newAlphaValueToAssign * 2;
                newGameItemOptionsTextTint.a += newAlphaValueToAssign * 2;
                break;
            
            case WhatInfoHasPriority.ruinOptions:
                newRuinOptionsBackgroundTint.a += newAlphaValueToAssign * 2;
                newRuinOptionsTextTint.a += newAlphaValueToAssign * 2;

                newRuinStoryBackgroundTint.a += newAlphaValueToAssign * 2;
                newRuinStoryTextTint.a += newAlphaValueToAssign * 2;
                break;
            
            case WhatInfoHasPriority.missingRuins:
                if (showMissingRuinsInfo)
                {
                    newMissingRuinsTextTint.a += newAlphaValueToAssign * 2;
                    newMissingRuinsBackgroundTint.a += newAlphaValueToAssign * 2;
                }
                break;
            case WhatInfoHasPriority.ruinStory:
                newRuinStoryBackgroundTint.a += newAlphaValueToAssign * 2;
                newRuinStoryTextTint.a += newAlphaValueToAssign * 2;
                break;
            case WhatInfoHasPriority.nothing:
            default:
                break;
        }

        RuinOptionsBackground.color = newRuinOptionsBackgroundTint;
        RuinOptionsText.color = newRuinOptionsTextTint;

        RuinStoryBackground.color = newRuinStoryBackgroundTint;
        RuinStoryText.color = newRuinStoryTextTint;

        GameItemOptionsBackground.color = newGameItemOptionsBackgroundTint;
        GameItemOptionsText.color = newGameItemOptionsTextTint;

        MissingRuinsText.color = newMissingRuinsTextTint;
        MissingRuinsBackground.color = newMissingRuinsBackgroundTint;



    }

    void RuinEntering()
    {
        isPlayerOnARuin = true;
        haveIImportedTheRuinStory = false;

        if (newRuinOptionsBackgroundTint.a < 0)
        {
            newRuinOptionsBackgroundTint.a = 0;
            newRuinOptionsTextTint.a = 0;
            newRuinStoryBackgroundTint.a = 0;
            newRuinStoryTextTint.a = 0;
        }
    }

    void FixedRuinEntering()
    {
        isPlayerOnAFixedRuin = true;
        haveIImportedTheRuinStory = false;

        if (newRuinStoryBackgroundTint.a < 0)
        {
            newRuinStoryBackgroundTint.a = 0;
            newRuinStoryTextTint.a = 0;
        }
    }

    void RuinExiting()
    {
        isPlayerOnARuin = false;
        isPlayerOnAFixedRuin = false;
        if (newRuinOptionsBackgroundTint.a > 1)
        {
            newRuinOptionsBackgroundTint.a = 1;
            newRuinOptionsTextTint.a = 1;
            newRuinStoryBackgroundTint.a = 1;
            newRuinStoryTextTint.a = 1;
        }
    }

    void BeingNearAMaterial()
    {
        isPlayerNearAMaterial = true;
        if (newGameItemOptionsBackgroundTint.a < 0)
        {
            newGameItemOptionsBackgroundTint.a = 0;
            newGameItemOptionsTextTint.a = 0;
        }
    }

    void NotBeingNearAMaterial()
    {
        isPlayerNearAMaterial = false;
        if (newGameItemOptionsBackgroundTint.a > 1)
        {
            newGameItemOptionsBackgroundTint.a = 1;
            newGameItemOptionsTextTint.a = 1;
        }
    }

    void BeingCloseToPyramid()
    {
        isPlayerNearThePyramid = true;
        if (newMissingRuinsBackgroundTint.a < 0)
        {
            newMissingRuinsBackgroundTint.a = 0;
            newMissingRuinsTextTint.a = 0;
        }
    }

    void SeparatingFromPyramid()
    {
        isPlayerNearThePyramid = false;
        if (newMissingRuinsBackgroundTint.a > 1)
        {
            newMissingRuinsBackgroundTint.a = 1;
            newMissingRuinsTextTint.a = 1;
            newRuinStoryBackgroundTint.a = 1;
            newRuinStoryTextTint.a = 1;
        }
    }

    void DoNotShowMissingRuinsInfo()
    {
        showMissingRuinsInfo = false;
    }

    public void AlterMissingRuinsMessage()
    {
        int uninteractedPhase1Ruins = GetComponent<PlayerScore>().UninteractedPhase1Ruins();
        int missingPhase2Ruins = 5 - GetComponent<PlayerScore>().pyramidControler.MissingPhase2Ruins();

        Debug.Log("Phase2 active is" + GetComponent<PlayerScore>().pyramidControler.hasPhase2Begun);

        if (GetComponent<PlayerScore>().pyramidControler.hasUserinteractedWithAllRuins)
        {
            MissingRuinsText.text = "Thank you...";
        }
        else if (GetComponent<PlayerScore>().pyramidControler.hasPhase2Begun)
        {
            MissingRuinsText.text = "You're missing " + missingPhase2Ruins + " ruins to be correctly placed..";
        }
        else
        {
            MissingRuinsText.text = "You're missing " + uninteractedPhase1Ruins + " ruins to ether repair or destroy and something else...";
        }
    }
}
