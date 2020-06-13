using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseReader : MonoBehaviour
{
    public int correctRuinID;
    public PyramidControler pyramidControler;
    public bool isPhase2Active = false,
                DoIHaveTheCorrectRuin = false;

    Color newColorToassign;

    private void OnTriggerStay(Collider other)
    {
       
       //if the ruin has the ID I want AND has been repaired,
        if (other.gameObject.GetComponent<Phase2Ruins>().ruinId == correctRuinID && other.GetComponent<Phase2Ruins>().haveIBeenRepaired)
        {
            newColorToassign = Color.green;

            DoIHaveTheCorrectRuin = true;

            pyramidControler.isThisRuinInTheCorrectPlaceAndRepaired[correctRuinID] = true;
            pyramidControler.CheckPhase2Puzzle();  
        }
        //if the ruin has tho correct Id BUT has not been repaired
        else if (other.gameObject.GetComponent<Phase2Ruins>().ruinId == correctRuinID && !other.GetComponent<Phase2Ruins>().haveIBeenRepaired)
        {
            newColorToassign = Color.yellow;

            pyramidControler.isThisRuinInTheCorrectPlaceAndRepaired[correctRuinID] = false;

            DoIHaveTheCorrectRuin = true;
        }
        //if I detect another ruin while I have the correct one already
        else if (other.gameObject.GetComponent<Phase2Ruins>().ruinId != correctRuinID && !DoIHaveTheCorrectRuin)
        {
            newColorToassign = Color.red;
            
            pyramidControler.isThisRuinInTheCorrectPlaceAndRepaired[correctRuinID] = false;
        }

        //BUG FIX
        if (!isPhase2Active)
            IntensityDeactivator(true);

        GetComponentInChildren<Light>().color = newColorToassign;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Phase2Ruins>() != null)
        {
            if (other.GetComponent<Phase2Ruins>().ruinId == correctRuinID)
            {
                GetComponentInChildren<Light>().color = Color.blue;

                pyramidControler.isThisRuinInTheCorrectPlaceAndRepaired[correctRuinID] = false;

                DoIHaveTheCorrectRuin = false;
            }
            else if (other.GetComponent<Phase2Ruins>().ruinId != correctRuinID && !DoIHaveTheCorrectRuin)
            {
                GetComponentInChildren<Light>().color = Color.blue;
            }
           
        }

        //BUG FIX
        if (!isPhase2Active)
            IntensityDeactivator(true);
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
