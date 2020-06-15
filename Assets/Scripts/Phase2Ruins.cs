using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Ruins : RepairableRuin
{
    public int ruinId;

    public Transform DestinyPoint;
    
    private void Awake()
    {
        amIAPhase2Ruin = true;
        PlayerScore.Phase2HasBegun += ChangePhase2Status;
    }


    new private void Update()
    {
        if (doIHavePriority)
        {
            priorityChangeFactor = Convert.ToSingle( Math.Sin(Time.time * 1.5) );
        }
        else
        {
            priorityChangeFactor = 1;
        }

        base.Update();
    }
    private void OnMouseDown()
    {
        var distance = transform.position - DestinyPoint.transform.position;

        Debug.Log("mag as" + distance.magnitude);

        if (distance.magnitude <= 20 && hasPhase2Begun)
        {
            amIBeingPickedUp = true;
            
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;

            GetComponent<MeshCollider>().enabled = false;

            transform.position = DestinyPoint.position;

            Debug.Log("using " + DestinyPoint.position + "as new position Vlaue");

            transform.rotation = new Quaternion(0, 0, 0, 0);
            transform.parent = DestinyPoint.transform;
        }
    }

    private void OnMouseUp()
    {
        amIBeingPickedUp = false;
        
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;

        GetComponent<MeshCollider>().enabled = true;
    }

    private void ChangePhase2Status()
    {
        hasPhase2Begun = true;
    }
}
