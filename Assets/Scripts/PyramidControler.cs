using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidControler : MonoBehaviour
{
    private void Start()
    {
        PlayerScore.HasInteractedWithAllRuins += UnlockDoors;
    }

    private void UnlockDoors()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

}
