using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidControler : MonoBehaviour
{
    MeshRenderer mRenderer;

    public bool hasUserinteractedWithAllRuins = false,
                hasNotChangedColorsYet = true;

    public bool[] isThisRuinInTheCorrectPlace = new bool[5];

    private void Awake()
    {
        mRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        PlayerScore.HasInteractedWithAllRuins += UnlockDoors;
        PlayerScore.HasInteractedWithAllRuins += PlayerHasWon;
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

}
