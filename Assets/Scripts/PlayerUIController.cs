using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    bool isPlayerOnARuin = false, isPlayerNearAMaterial = false;

    public Text RuinOptionsText, MaterialOptionsText;
    public Image RuinOptionsBackground, MaterialOptionsBackground; 

    Color ruinOptionsBackgroundTint, materialOptionsBackgroundTint;
    Color ruinOptionsTextTint, materialOptionsTextTint;

    readonly float changeFactor = 2;

    void Start()
    {
        //subscribe functions to events
        PlayerMovement.IAmInARuin += RuinEntering;
        PlayerMovement.IAmNotInARuin += RuinExiting;
        PlayerMovement.IAmNearAMaterial += BeingNearAMaterial;
        PlayerMovement.IAmNotNearAMaterial += NotBeingNearAMaterial;

        //copy current color values to avoid discrepancies
        ruinOptionsBackgroundTint = RuinOptionsBackground.color;
        ruinOptionsTextTint = RuinOptionsText.color;

        materialOptionsBackgroundTint = MaterialOptionsBackground.color;
        materialOptionsTextTint = MaterialOptionsText.color;

        //set colors's alpha to 0
        ruinOptionsBackgroundTint.a = 0;
        ruinOptionsTextTint.a = 0;

        materialOptionsBackgroundTint.a = 0;
        materialOptionsTextTint.a = 0;
    }

    private void Update()
    {
        //Debug.Log("When entering the update function, I read TPIONAR as " + isPlayerOnARuin + " and IPNAM as " + isPlayerNearAMaterial);

        if (isPlayerNearAMaterial)
        {
            materialOptionsBackgroundTint.a -= Time.deltaTime * changeFactor;
            materialOptionsTextTint.a -= Time.deltaTime * changeFactor;
        }


        Debug.Log("Alpha vaules I take: MatT:" + materialOptionsTextTint.a + " MatB:" + materialOptionsBackgroundTint.a + " RuiT:" + ruinOptionsTextTint.a + "RuiB:" + ruinOptionsBackgroundTint.a);

        /*if (ruinOptionsBackgroundTint.a < 0)
        {
            ruinOptionsBackgroundTint.a = 0;
            ruinOptionsTextTint.a = 0;
        }
        if (ruinOptionsBackgroundTint.a > 1)
        {
            ruinOptionsBackgroundTint.a = 1;
            ruinOptionsTextTint.a = 1;
        }
        if (materialOptionsBackgroundTint.a < 0)
        {
            materialOptionsBackgroundTint.a = 0;
            materialOptionsTextTint.a = 0;
        }
        if (materialOptionsBackgroundTint.a > 1)
        {
            materialOptionsBackgroundTint.a = 1;
            materialOptionsTextTint.a = 1;
        }*/



        //asign new values
        RuinOptionsBackground.color = ruinOptionsBackgroundTint;
        RuinOptionsText.color = ruinOptionsTextTint;
        MaterialOptionsBackground.color = ruinOptionsBackgroundTint;
        MaterialOptionsText.color = ruinOptionsTextTint;



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
