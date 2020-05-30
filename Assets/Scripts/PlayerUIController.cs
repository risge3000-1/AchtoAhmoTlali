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

    Color ruinOptionsBackgroundTint;
    Color gameItemOptionsBackgroundTint;
    Color ruinOptionsTextTint;
    Color gameItemOptionsTextTint;
    Color missingRuinsTextTint;
    Color missingRuinsBackgroundTint;

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
        ruinOptionsBackgroundTint = RuinOptionsBackground.color;
        ruinOptionsTextTint = RuinOptionsText.color;

        gameItemOptionsBackgroundTint = GameItemOptionsBackground.color;
        gameItemOptionsTextTint = GameItemOptionsText.color;

        missingRuinsBackgroundTint = MissingRuinsBackground.color;
        missingRuinsTextTint = MissingRuinsText.color;

        //set colors's alpha to 0
        ruinOptionsBackgroundTint.a = 0;
        ruinOptionsTextTint.a = 0;

        gameItemOptionsBackgroundTint.a = 0;
        gameItemOptionsTextTint.a = 0;

        missingRuinsTextTint.a = 0;
        missingRuinsBackgroundTint.a = 0;

        AlterMissingRuinsMessage();
    }

    private void Update()
    {
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
        float NewValueToAssign = Time.deltaTime * changeFactor;

        gameItemOptionsBackgroundTint.a -= NewValueToAssign;
        gameItemOptionsTextTint.a -= NewValueToAssign;

        ruinOptionsBackgroundTint.a -= NewValueToAssign;
        ruinOptionsTextTint.a -= NewValueToAssign;

        missingRuinsTextTint.a -= NewValueToAssign;
        missingRuinsBackgroundTint.a -= NewValueToAssign;

        switch (infoThatHasToBePrioritized)
        {
            case WhatInfoHasPriority.itemOptions:
                gameItemOptionsBackgroundTint.a += NewValueToAssign * 2;
                gameItemOptionsTextTint.a += NewValueToAssign * 2;
                break;
            
            case WhatInfoHasPriority.ruinOptions:
                ruinOptionsBackgroundTint.a += NewValueToAssign * 2;
                ruinOptionsTextTint.a += NewValueToAssign * 2;
                break;
            
            case WhatInfoHasPriority.missingRuins:
                if (showMissingRuinsInfo)
                {
                    missingRuinsTextTint.a += NewValueToAssign * 2;
                    missingRuinsBackgroundTint.a += NewValueToAssign * 2;
                }
                break;
            
            case WhatInfoHasPriority.nothing:
            default:
                break;
        }




        if (ruinOptionsBackgroundTint.a < 0)
        {
            ruinOptionsBackgroundTint.a = 0;
            ruinOptionsTextTint.a = 0;
        }
        else if (ruinOptionsBackgroundTint.a > 1)
        {
            ruinOptionsBackgroundTint.a = 1;
            ruinOptionsTextTint.a = 1;
        }
        
        if (gameItemOptionsBackgroundTint.a < 0)
        {
            gameItemOptionsBackgroundTint.a = 0;
            gameItemOptionsTextTint.a = 0;
        }
        else if (gameItemOptionsBackgroundTint.a > 1)
        {
            gameItemOptionsBackgroundTint.a = 1;
            gameItemOptionsTextTint.a = 1;
        }

        if (missingRuinsBackgroundTint.a < 0)
        {
            missingRuinsBackgroundTint.a = 0;
            missingRuinsTextTint.a = 0;
        }
        else if (missingRuinsBackgroundTint.a > 1)
        {
            missingRuinsBackgroundTint.a = 1;
            missingRuinsTextTint.a = 1;
        }



        RuinOptionsBackground.color = ruinOptionsBackgroundTint;
        RuinOptionsText.color = ruinOptionsTextTint;
        GameItemOptionsBackground.color = gameItemOptionsBackgroundTint;
        GameItemOptionsText.color = gameItemOptionsTextTint;
        MissingRuinsText.color = missingRuinsTextTint;
        MissingRuinsBackground.color = missingRuinsBackgroundTint;
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
        infoAmoutMissingRuins = ("Te faltan " + (PlayerScore.minimalRuinsTointeractWith - PlayerScore.staticRuinsPlayerHasIteractedWith) + " Ruinas por interactuar");
        MissingRuinsText.text = infoAmoutMissingRuins;
    }
}
