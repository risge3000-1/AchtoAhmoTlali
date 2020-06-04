using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SunRotation : MonoBehaviour
{
    public bool isRotationActive = true;
    public static bool isItNight = false;

    // Update is called once per frame
    void Update()
    {
        if (isRotationActive)
            transform.Rotate(Vector3.right * Time.deltaTime);

        //if rotation value is between 15 and 0 (inclusive) or between 360 and 270 (also inclusive), say it is night. Otherwise, it's not night
        //Uncomment the line below to know why the rotation is set like that. I don't unrestand that particular Unity logic as well
        //Debug.Log("I am reading eulerAngles x rotation as " + transform.rotation.eulerAngles.x); 

        if ((transform.rotation.eulerAngles.x <= 15 && transform.rotation.eulerAngles.x >= 0 )|| (transform.rotation.eulerAngles.x <= 360 && transform.rotation.eulerAngles.x >= 270))
            isItNight = true;
        else
            isItNight = false;
    }
}
