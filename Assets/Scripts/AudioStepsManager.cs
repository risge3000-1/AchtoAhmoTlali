using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStepsManager : MonoBehaviour
{
    public AudioClip WaterSteps, TerrainSteps;
    public AudioSource StepsAudioSurce;

    bool alreadyDidOnce = false,
         aKeyIsBeingPressed = false;

    private void Awake()
    {
         StepsAudioSurce.clip = TerrainSteps;
    }

    private void Update()
    {
        /*If a movement key started or stopped getting pressed*/
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) ||  (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)))
            alreadyDidOnce = false;

        /*if a key is being pressed*/
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            aKeyIsBeingPressed = true;
        else
            aKeyIsBeingPressed = false;        


        if (aKeyIsBeingPressed && !alreadyDidOnce && !StepsAudioSurce.isPlaying)
        {
            PlaySound(true);
            alreadyDidOnce = true;
        }
        else if (!aKeyIsBeingPressed)
        {
            PlaySound(false);
        }
    }

    public void SwitchToWater()
    {
        PlaySound(false);
        StepsAudioSurce.clip = WaterSteps;
        PlaySound(true);
    }

    public void SwitchToDirt()
    {
        PlaySound(false);
        StepsAudioSurce.clip = TerrainSteps;
        PlaySound(true);
    }

    void PlaySound(bool Activate)
    {
        if (Activate)
            StepsAudioSurce.Play();
        
        else
            StepsAudioSurce.Stop(); 
    }
}
