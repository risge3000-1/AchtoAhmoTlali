using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseReader : MonoBehaviour
{
    public int correctRuinID;
    public PyramidControler pyramidControler;
    public bool isPhase2Active = false;
    Color newColorToassign;

    private void OnTriggerEnter(Collider other)
    {
       
       //if the ruin has the ID I want AND has been repaired,
        if (other.gameObject.GetComponent<Phase2Ruins>().ruinId == correctRuinID && other.GetComponent<Phase2Ruins>().haveIBeenRepaired)
        {
            newColorToassign = Color.green;

            pyramidControler.isThisRuinInTheCorrectPlace[correctRuinID] = true;
            pyramidControler.CheckPhase2Puzzle();  
        }
        else if (other.gameObject.GetComponent<Phase2Ruins>().ruinId != correctRuinID)
        {
            newColorToassign = Color.red;
            
            pyramidControler.isThisRuinInTheCorrectPlace[correctRuinID] = false;
        }

        if (!isPhase2Active)
            IntensityDeactivator(true);

        Debug.Log("a as" + newColorToassign.a + " and phase2Active as " + isPhase2Active);

        GetComponentInChildren<Light>().color = newColorToassign;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Phase2Ruins>() != null)
        {
            GetComponentInChildren<Light>().color = Color.yellow;
            
            if (!isPhase2Active)
                IntensityDeactivator(true);
            
            pyramidControler.isThisRuinInTheCorrectPlace[correctRuinID] = false;
        }
    }

    public void ChangesForPhase2()
    {
        IntensityDeactivator(false);
        isPhase2Active = true;

        GetComponentInChildren<Light>().color = Color.blue;
        StartCoroutine(WaitSeconds(2));
        GetComponentInChildren<Light>().color = newColorToassign;
    }

    IEnumerator WaitSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void IntensityDeactivator(bool deactivateLight)
    {
        if (deactivateLight)
            GetComponentInChildren<Light>().intensity = 0;
        else
            GetComponentInChildren<Light>().intensity = 1.25f;
    }
}
