using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    bool isPlayerOnARuin = false, isPlayerNearAMaterial = false, isPlayerNearThePyramid = false, showMissingRuinsInfo = true;

    public enum WhatInfoHasPriority
    {
        itemOptions,
        ruinOptions,
        missingRuins,
        nothing

    }

    public Text RuinOptionsText;
    public Image RuinOptionsBackground;
    public Text  GameItemOptionsText;
    public Image GameItemOptionsBackground;
    public Text MissingRuinsText;
    public Image MissingRuinsBackground;

    public string infoAmoutMissingRuins;

    Color newRuinOptionsBackgroundTint;
    Color newGameItemOptionsBackgroundTint;
    Color newRuinOptionsTextTint;
    Color newGameItemOptionsTextTint;
    Color newMissingRuinsTextTint;
    Color newMissingRuinsBackgroundTint;

    readonly float changeFactor = 2;

    void Start()
    {
        //subscribe functions to events
        PlayerMovement.IAmInABrokenRuin += RuinEntering;
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

        //set colors's alpha to 0
        newRuinOptionsBackgroundTint.a = 0;
        newRuinOptionsTextTint.a = 0;

        newGameItemOptionsBackgroundTint.a = 0;
        newGameItemOptionsTextTint.a = 0;

        newMissingRuinsTextTint.a = 0;
        newMissingRuinsBackgroundTint.a = 0;

    }

    private void Update()
    {
        //This is Only to fix the bug for when the payer interacts with te first ruin.
        AlterMissingRuinsMessage();

        //Debug.Log("When entering the update function, I read TPIONAR as " + isPlayerOnARuin + " and IPNAM as " + isPlayerNearAMaterial);

        if (isPlayerNearAMaterial && !isPlayerOnARuin)
        {
            ColorClearnessManager(WhatInfoHasPriority.itemOptions);
        }
        else if (isPlayerNearThePyramid)
        {
            ColorClearnessManager(WhatInfoHasPriority.missingRuins);
        }
        else if (isPlayerOnARuin)
        {
            ColorClearnessManager(WhatInfoHasPriority.ruinOptions);
        }
        else
        {
            ColorClearnessManager(WhatInfoHasPriority.nothing);
        }

 
    }

    void ColorClearnessManager(WhatInfoHasPriority infoThatHasToBePrioritized)
    {
        float newAlphaValueToAssign = Time.deltaTime * changeFactor;

        newGameItemOptionsBackgroundTint.a -= newAlphaValueToAssign;
        newGameItemOptionsTextTint.a -= newAlphaValueToAssign;

        newRuinOptionsBackgroundTint.a -= newAlphaValueToAssign;
        newRuinOptionsTextTint.a -= newAlphaValueToAssign;

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
                break;
            
            case WhatInfoHasPriority.missingRuins:
                if (showMissingRuinsInfo)
                {
                    newMissingRuinsTextTint.a += newAlphaValueToAssign * 2;
                    newMissingRuinsBackgroundTint.a += newAlphaValueToAssign * 2;
                }
                break;
            
            case WhatInfoHasPriority.nothing:
            default:
                break;
        }




        if (newRuinOptionsBackgroundTint.a < 0)
        {
            newRuinOptionsBackgroundTint.a = 0;
            newRuinOptionsTextTint.a = 0;
        }
        else if (newRuinOptionsBackgroundTint.a > 1)
        {
            newRuinOptionsBackgroundTint.a = 1;
            newRuinOptionsTextTint.a = 1;
        }
        
        if (newGameItemOptionsBackgroundTint.a < 0)
        {
            newGameItemOptionsBackgroundTint.a = 0;
            newGameItemOptionsTextTint.a = 0;
        }
        else if (newGameItemOptionsBackgroundTint.a > 1)
        {
            newGameItemOptionsBackgroundTint.a = 1;
            newGameItemOptionsTextTint.a = 1;
        }

        if (newMissingRuinsBackgroundTint.a < 0)
        {
            newMissingRuinsBackgroundTint.a = 0;
            newMissingRuinsTextTint.a = 0;
        }
        else if (newMissingRuinsBackgroundTint.a > 1)
        {
            newMissingRuinsBackgroundTint.a = 1;
            newMissingRuinsTextTint.a = 1;
        }



        RuinOptionsBackground.color = newRuinOptionsBackgroundTint;
        RuinOptionsText.color = newRuinOptionsTextTint;
        GameItemOptionsBackground.color = newGameItemOptionsBackgroundTint;
        GameItemOptionsText.color = newGameItemOptionsTextTint;
        MissingRuinsText.color = newMissingRuinsTextTint;
        MissingRuinsBackground.color = newMissingRuinsBackgroundTint;
    }

    void RuinEntering()
    {
        isPlayerOnARuin = true;
    }

    void RuinExiting()
    {
        isPlayerOnARuin = false;  
    }

    void BeingNearAMaterial()
    {
        isPlayerNearAMaterial = true;   
    }

    void NotBeingNearAMaterial()
    {
        isPlayerNearAMaterial = false;     
    }

    void BeingCloseToPyramid()
    {
        isPlayerNearThePyramid = true;
    }

    void SeparatingFromPyramid()
    {
        isPlayerNearThePyramid = false;
    }

    void DoNotShowMissingRuinsInfo()
    {
        showMissingRuinsInfo = false;
    }

    void AlterMissingRuinsMessage()
    {
        int numberOfMissingRuins = PlayerScore.minimalRuinsTointeractWith - PlayerScore.staticRuinsPlayerHasIteractedWith;
        
        if (numberOfMissingRuins <= 0)
            infoAmoutMissingRuins = "You Won! The game will close itself on a moment";
        else
            infoAmoutMissingRuins = ("You're missing " + numberOfMissingRuins + " ruins to interact with");
           
        
        MissingRuinsText.text = infoAmoutMissingRuins;
    }
}
