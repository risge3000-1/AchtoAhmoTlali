using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidControler : MonoBehaviour
{
    MeshRenderer mRenderer;

    public Phase2Ruins[] Phase2Ruins = new Phase2Ruins[5];

    public bool hasUserinteractedWithAllRuins = false,
                hasNotChangedColorsYet = true,
                hasUserPutAllRuinsInTheCorrectPlace = false,
                hasPhase2Begun = false;
    

    public bool[] isThisRuinInTheCorrectPlace = new bool[5];

    public string messageAboutTheRuins;

    private void Awake()
    {
        mRenderer = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        if (hasNotChangedColorsYet)
        {
            if (hasUserinteractedWithAllRuins)
                mRenderer.material.color = Color.cyan;
            else
                mRenderer.material.color = Color.yellow;

            hasNotChangedColorsYet = false;
        }
    }

    private void UnlockDoors()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        hasNotChangedColorsYet = true;
    }

    private void PlayerHasWon()
    {
        hasUserinteractedWithAllRuins = true;
    }

    public void CheckPhase2Puzzle()
    {
        int ruinsThatHaveBeenCorrectlyPlaced = 0;

        for (int i = 0; i < 5; i++)
        {
            if (isThisRuinInTheCorrectPlace[i])
            {
                ruinsThatHaveBeenCorrectlyPlaced++;
            }
        }

        if (ruinsThatHaveBeenCorrectlyPlaced == 5)
        {
            PlayerHasWon();
            UnlockDoors();
        }
    }

    public void AnnouncePhase2Beggining()
    {
        for (int i = 0; i < 5; i++)
        {
            Phase2Ruins[i].isPhase2Active = true;
        }
    }

}
