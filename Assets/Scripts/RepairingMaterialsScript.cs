using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairingMaterialsScript : MonoBehaviour
{
    bool isPlayerOnARuin = false;
    
    private void Start()
    {
        PlayerMovement.IAmInABrokenRuin += ThereIsARuinOvelappingMe;
        PlayerMovement.IAmNotInARuin += NoRuinOvelapsMeAnymore;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null && !isPlayerOnARuin)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddToInventory();
            }
        }
    }

    void ThereIsARuinOvelappingMe()
    {
        isPlayerOnARuin = true;
    }

    void NoRuinOvelapsMeAnymore()
    {
        isPlayerOnARuin = false;
    }

    void AddToInventory()
    {
        PlayerMovement.aGameItemGotPickedUp = true;
        gameObject.SetActive(false);
    }
}
