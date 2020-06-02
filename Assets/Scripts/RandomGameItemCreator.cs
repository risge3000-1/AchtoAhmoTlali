using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGameItemCreator : MonoBehaviour
{
    public GameObject LocalWood, LocalStone, LocalRopes, LocalClay;

    static public GameObject StaticWood, StaticStone, StaticRopes, StaticClay;

    private void Awake()
    {
        StaticWood = LocalWood;
        StaticStone = LocalStone;
        StaticRopes = LocalRopes;
        StaticClay = LocalClay;
    }

    static public void GenerateRandomMaterial(float xPosition, float yPosition, float zPosition)
    {
        int randomFactor = 0 /*Random.Range(0,3)*/;
        switch (randomFactor)
        {
            case 0:
                Instantiate(StaticWood, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
                
                break;
            case 1:
                Instantiate(StaticStone, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
                break;
            case 2:
                Instantiate(StaticRopes, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
                break;
            case 3:
                Instantiate(StaticClay, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
                break;
            default:
                Debug.LogError("Random Factor in Random Item generatos is" + randomFactor);
                break;
        }
    }
}
