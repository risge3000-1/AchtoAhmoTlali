using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepairableRuin : MonoBehaviour
{
    public bool haveIBeenRepaired = false, 
                hasNotChangedColorsYet = true;

    MeshRenderer mRenderer;

    private void Awake()
    {
        mRenderer = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        if (hasNotChangedColorsYet)
        {
            if (haveIBeenRepaired)
                mRenderer.material.color = Color.green;
            else
                mRenderer.material.color = Color.red;

            hasNotChangedColorsYet = false;
        }
    }

    public void RepairRuin()
    {
        haveIBeenRepaired = true;
        hasNotChangedColorsYet = true;
    }

    public void DestroyRuin()
    {
        gameObject.SetActive(false);   
    }
}
