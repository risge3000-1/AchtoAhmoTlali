using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStepsManager : MonoBehaviour
{
    public AudioClip WaterSteps, TerrainSteps;
    public AudioSource StepsAudioSurce;

    private void Awake()
    {
         StepsAudioSurce.clip = TerrainSteps;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            PlaySound(true);

        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            PlaySound(false);
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
        {
            StepsAudioSurce.Play();
        }
        else
        {
            StepsAudioSurce.Stop();
        }
    }
}
