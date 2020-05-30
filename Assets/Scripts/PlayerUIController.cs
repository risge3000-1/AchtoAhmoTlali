using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    bool isPlayerOnARuin = false, isPlayerNearAMaterial = false;

    public Text RuinOptionsText;
    public Image RuinOptionsBackground;
    public Text  GameItemOptionsText;
    public Image GameItemOptionsBackground;

    Color ruinOptionsBackgroundTint;
    Color gameItemOptionsBackgroundTint;
    Color ruinOptionsTextTint;
    Color gameItemOptionsTextTint;

    readonly float changeFactor = 2;

    void Start()
    {
        //subscribe functions to events
        PlayerMovement.IAmInABrokenRuin += RuinEntering;
        PlayerMovement.IAmNotInARuin += RuinExiting;
        PlayerMovement.IAmNearAMaterial += BeingNearAMaterial;
        PlayerMovement.IAmNotNearAMaterial += NotBeingNearAMaterial;

        //copy current color values to avoid discrepancies
        ruinOptionsBackgroundTint = RuinOptionsBackground.color;
        ruinOptionsTextTint = RuinOptionsText.color;

        gameItemOptionsBackgroundTint = GameItemOptionsBackground.color;
        gameItemOptionsTextTint = GameItemOptionsText.color;

        //set colors's alpha to 0
        ruinOptionsBackgroundTint.a = 0;
        ruinOptionsTextTint.a = 0;

        gameItemOptionsBackgroundTint.a = 0;
        gameItemOptionsTextTint.a = 0;
    }

    private void Update()
    {
        //Debug.Log("When entering the update function, I read TPIONAR as " + isPlayerOnARuin + " and IPNAM as " + isPlayerNearAMaterial);

        if (isPlayerNearAMaterial && !isPlayerOnARuin)
        {
            gameItemOptionsBackgroundTint.a += Time.deltaTime * changeFactor;
            gameItemOptionsTextTint.a += Time.deltaTime * changeFactor;

            ruinOptionsBackgroundTint.a -= Time.deltaTime * changeFactor;
            ruinOptionsTextTint.a -= Time.deltaTime * changeFactor;
        }
        else if (isPlayerOnARuin)
        {
            gameItemOptionsBackgroundTint.a -= Time.deltaTime * changeFactor;
            gameItemOptionsTextTint.a -= Time.deltaTime * changeFactor;

            ruinOptionsBackgroundTint.a += Time.deltaTime * changeFactor;
            ruinOptionsTextTint.a += Time.deltaTime * changeFactor;
        }
        else
        {
            gameItemOptionsBackgroundTint.a -= Time.deltaTime * changeFactor;
            gameItemOptionsTextTint.a -= Time.deltaTime * changeFactor;

            ruinOptionsBackgroundTint.a -= Time.deltaTime * changeFactor;
            ruinOptionsTextTint.a -= Time.deltaTime * changeFactor;
        }

        Debug.Log("IsPlayerOnARuin as " + isPlayerOnARuin + " and IsPlayerNearAMaterial as " + isPlayerNearAMaterial);

        if (ruinOptionsBackgroundTint.a < 0)
        {
            ruinOptionsBackgroundTint.a = 0;
            ruinOptionsTextTint.a = 0;
        }
        if (ruinOptionsBackgroundTint.a > 1)
        {
            ruinOptionsBackgroundTint.a = 1;
            ruinOptionsTextTint.a = 1;
        }
        if (gameItemOptionsBackgroundTint.a < 0)
        {
            gameItemOptionsBackgroundTint.a = 0;
            gameItemOptionsTextTint.a = 0;
        }
        if (gameItemOptionsBackgroundTint.a > 1)
        {
            gameItemOptionsBackgroundTint.a = 1;
            gameItemOptionsTextTint.a = 1;
        }



        //asign new values
        RuinOptionsBackground.color = ruinOptionsBackgroundTint;
        RuinOptionsText.color = ruinOptionsTextTint;
        GameItemOptionsBackground.color = gameItemOptionsBackgroundTint;
        GameItemOptionsText.color = gameItemOptionsTextTint;



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
}
