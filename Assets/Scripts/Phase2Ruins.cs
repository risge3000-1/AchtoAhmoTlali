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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            hasPhase2Begun = true;
        }

        base.Update();
    }
    private void OnMouseDown()
    {
        var distance = transform.position - GameObject.Find("PickingPositionObject").transform.position;
        
        if (distance.magnitude <= 6 && hasPhase2Begun)
        {
            amIBeingPickedUp = true;
            
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;

            GetComponent<MeshCollider>().enabled = false;

            transform.position = DestinyPoint.position;
            transform.parent = GameObject.Find("PickingPositionObject").transform;
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
