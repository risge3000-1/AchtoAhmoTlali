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
        int randomFactor = Random.Range(0,3);
        GameObject materialToUse;

        switch (randomFactor)
        {
            case 0: /*Wood*/
                materialToUse = StaticWood;
                break;
            case 1:/*Stone*/
                materialToUse = StaticStone;
                break;
            case 2: /*Ropes*/
                materialToUse = StaticRopes;
                break;
            case 3:/*Clay*/
                materialToUse = StaticClay;
                break;
            default:
                Debug.LogError("Random Factor in Random Item generatos is" + randomFactor + ", using wood as Material");
                materialToUse = StaticWood;
                break;
        }

        var obj = Instantiate(materialToUse, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);

        obj.GetComponent<RepairingMaterialsScript>().IWasGeneratedFromARuinDestruction();
    }
}
