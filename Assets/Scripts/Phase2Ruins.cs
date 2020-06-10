using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Ruins : RepairableRuin
{
    public int ruinId;

    public Transform DestinyPoint;

    public bool isPhase2Active = true;

    private void Awake()
    {
        amIAPhase2Ruin = true;
        //isPhase2Active = true;
    }

    private void OnMouseDown()
    {
        var distance = transform.position - GameObject.Find("PickingPositionObject").transform.position;

        Debug.Log("I read the magnitude as " + distance.magnitude);

        if (distance.magnitude <= 6 && isPhase2Active)
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
}
