using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEvents : MonoBehaviour
{
    public Image blackout;
    public AudioSource backgroundMusic;
    public Text pointer;

    public Image[] decoBackgrounds = new Image[3];
    public Text[] decoTexts = new Text[3];
    public Image[] backgrounds = new Image[3];

    public GameObject SlotsManager;

    Color newBlackoutColor,
          newDecoBackgroundColor,
          newDecoTextColor,
          newBackgroundColor,
          newPointerColor;

    private void Awake()
    {
        newBlackoutColor = blackout.color;
        newBlackoutColor.a = 1;

        /*Initialize colors with the current color values to avoid discrepancies*/
        newDecoBackgroundColor = decoBackgrounds[0].color;
        newDecoTextColor = decoTexts[0].color;
        newBackgroundColor = backgrounds[0].color;
        newPointerColor = pointer.color;

        
    }

    IEnumerator GetOutFromBlackout()
    {
        SlotsManager.SetActive(false);

        /*make all colors transparent*/
        newDecoBackgroundColor.a = 0;
        newDecoTextColor.a = 0;
        newBackgroundColor.a = 0;

        newPointerColor.a = 0;

        //assign transparented colors
        for (int i = 0; i < 3; i++)
        {
            decoBackgrounds[i].color = newDecoBackgroundColor;
            decoTexts[i].color = newDecoTextColor;
            backgrounds[i].color = newBackgroundColor;
        }
        pointer.color = newPointerColor;

        //make the fake blackout dissapear 
        for (float newAlpha = 1; newAlpha > 0; newAlpha -= Time.deltaTime / 2)
        {
            newBlackoutColor.a = newAlpha;
            blackout.color = newBlackoutColor;
            yield return null;
        }

        backgroundMusic.Play();

        //make pointer appear
        for (float newAlpha = 0; newAlpha < 1; newAlpha += Time.deltaTime)
        {
            newPointerColor.a = newAlpha;
            pointer.color = newPointerColor;
            yield return null;
        }

        /*Keep on loop while the alpha doesn't reach the desired value for the Deco Backgrounds*/
        for (float newDecoBGAlpha = 0; newDecoBGAlpha < 0.5f; newDecoBGAlpha += Time.deltaTime / 2)
        {
            newDecoBackgroundColor.a = newDecoBGAlpha;
            
            for (int i = 0; i < 3; i++)
                decoBackgrounds[i].color = newDecoBackgroundColor;
            
            yield return null;
        }

        /*Keep on loop while the alpha doesn't reach the desired value for the Deco Texts*/
        for (float newDecoTextAlpha = 0; newDecoTextAlpha < 1; newDecoTextAlpha += Time.deltaTime)
        {
            newDecoTextColor.a = newDecoTextAlpha;

            for (int i = 0; i < 3; i++)
                decoTexts[i].color = newDecoTextColor;

            yield return null;
        }

        /*Keep on loop while the alpha doesn't reach the desired value for the real Backgrounds*/
        for (float newBGAlpha = 0; newBGAlpha < 0.5f; newBGAlpha += Time.deltaTime / 2)
        {
            newBackgroundColor.a = newBGAlpha;

            for (int i = 0; i < 3; i++)
                backgrounds[i].color = newBackgroundColor;

            yield return null;
        }

        SlotsManager.SetActive(true);
        SlotsManager.GetComponentInParent<InventoryUI>().CreateSlots();

    }

    public void QuitBlackout()
    {
        StartCoroutine(GetOutFromBlackout());
    }
}
