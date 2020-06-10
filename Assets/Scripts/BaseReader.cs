using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseReader : MonoBehaviour
{
    public int correctRuinID;
    public PyramidControler pyramidControler;
    public bool isPhase2Active = false;

    private void Awake()
    {
        ColorCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Phase2Ruins>().ruinId == correctRuinID && other.GetComponent<Phase2Ruins>().haveIBeenRepaired)
        {
            GetComponentInChildren<Light>().color = Color.green;
            pyramidControler.isThisRuinInTheCorrectPlace[correctRuinID] = true;
            pyramidControler.CheckPhase2Puzzle();
        }
        else if (other.gameObject.GetComponent<Phase2Ruins>().ruinId != correctRuinID)
        {
            GetComponentInChildren<Light>().color = Color.red;
            pyramidControler.isThisRuinInTheCorrectPlace[correctRuinID] = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Phase2Ruins>() != null)
        {
            GetComponentInChildren<Light>().color = Color.yellow;
            pyramidControler.isThisRuinInTheCorrectPlace[correctRuinID] = false;
        }
    }

    private void ColorCheck()
    {
        if (!isPhase2Active)
        {
            GetComponentInChildren<Light>().color = Color.clear;
        }
        else
        {
            GetComponentInChildren<Light>().color = Color.white;
        }
    }
}
