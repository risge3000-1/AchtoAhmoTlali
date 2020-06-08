using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseReader : MonoBehaviour
{
    public int correcrtRuinID;
    public PyramidControler pyramidControler;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Phase2Ruins>().ruinId == correcrtRuinID)
        {
            GetComponentInChildren<Light>().color = Color.green;
            pyramidControler.isThisRuinInTheCorrectPlace[correcrtRuinID] = true;
        }
        else if (other.gameObject.GetComponent<Phase2Ruins>().ruinId != correcrtRuinID)
        {
            GetComponentInChildren<Light>().color = Color.red;
            pyramidControler.isThisRuinInTheCorrectPlace[correcrtRuinID] = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Phase2Ruins>() != null)
        {
            GetComponentInChildren<Light>().color = Color.yellow;
            pyramidControler.isThisRuinInTheCorrectPlace[correcrtRuinID] = false;
        }
    }
}
