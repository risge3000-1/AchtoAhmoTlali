using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item item;
    private Image spriteImage;

    Image slotBG ;
    Color newColor;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        slotBG = transform.parent.GetComponent<Image>();
        newColor = slotBG.color;

        newColor.a = 0;
    }

    public void UpdateItem(Item item)

    {
        this.item = item;


        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
           

        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }

    public void GraduallyAppear()
    {
        StartCoroutine(GraddualyColor());
    }

    IEnumerator GraddualyColor()
    {
        
        for (float newAlpha = 0; newAlpha < 1; newAlpha += Time.deltaTime * 1.5f)
        {
            newColor.a = newAlpha;
            slotBG.color = newColor;
            yield return null;
        }
    }
}
