using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int ruinsPlayerHasIteractedWith = 20;
    public int repairedRuins = 0;
    public int destroyedRuins = 0;
    static public readonly int minimalRuinsTointeractWith = 21;
    static public int staticRuinsPlayerHasIteractedWith;

    public delegate void ScoreEvents();
    public static event ScoreEvents HasInteractedWithAllRuins;

    public void Start()
    {
        //subscribe functions to events
        PlayerMovement.IDestroyedARuin += IncreaseDestroyedRuins;
        PlayerMovement.IrepairedARuin += IncreaseRepairedRuins;   
    }

    public void IncreaseDestroyedRuins()
    {
        destroyedRuins++;
        CheckTotalScore();
    }

    public void IncreaseRepairedRuins()
    {
        repairedRuins++;
        CheckTotalScore();
    }

    public void CheckTotalScore()
    {
        ruinsPlayerHasIteractedWith++;
        staticRuinsPlayerHasIteractedWith = ruinsPlayerHasIteractedWith;

        if (ruinsPlayerHasIteractedWith >= minimalRuinsTointeractWith)
        {
            HasInteractedWithAllRuins();
        }

    } 

}
