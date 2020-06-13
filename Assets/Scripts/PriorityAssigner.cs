using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityAssigner : MonoBehaviour
{
    public Phase2Ruins[] phase2Ruin = new Phase2Ruins[5];

    public void DefinePriority()
    {
        float lastLenght = 1000;

        Debug.Log("I enter the priority Function");

        for (int i = 0; i < 5; i++)
        {
            var distance = transform.position - phase2Ruin[i].transform.position;

            phase2Ruin[i].doIHavePriority = false;

            if (distance.magnitude <= lastLenght && !phase2Ruin[i].haveIBeenRepaired)
            {

                //if the index doesn't get out
                if (i - 1 >= 0) phase2Ruin[i - 1].doIHavePriority = false;

                phase2Ruin[i].doIHavePriority = true;
                lastLenght = distance.magnitude;

                Debug.Log("Ruin " + (i + 1) + " now has the priority");
            }
        }

    }

}
