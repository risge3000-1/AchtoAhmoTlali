using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidControler : MonoBehaviour
{
    MeshRenderer mRenderer;

    public BaseReader[] ReadingBases = new BaseReader[5];

    public bool hasUserinteractedWithAllRuins = false,
                hasNotChangedColorsYet = true,
                hasUserPutAllRuinsInTheCorrectPlace = false,
                hasPhase2Begun = false;

    public delegate void PyramidLocationActions();
    public static event PyramidLocationActions PlayerIsNearThePyramid, PlayerIsNotNearThePyramid;
    

    public bool[] isThisRuinInTheCorrectPlace = new bool[5];

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            PlayerIsNearThePyramid();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            PlayerIsNotNearThePyramid();
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

        if (MissingPhase2Ruins() >= 5)
        {
            PlayerHasWon();
            UnlockDoors();
        }
    }

    public void AnnouncePhase2Beggining()
    {
        for (int i = 0; i < 5; i++)
        {
            ReadingBases[i].ChangesForPhase2();
        }
        hasPhase2Begun = true;
    }

    public int MissingPhase2Ruins()
    {
        int ruinsThatHaveBeenCorrectlyPlaced = 0;

        for (int i = 0; i < 5; i++)
        {
            if (isThisRuinInTheCorrectPlace[i])
            {
                ruinsThatHaveBeenCorrectlyPlaced++;
            }
        }

        return ruinsThatHaveBeenCorrectlyPlaced;
    }
}
