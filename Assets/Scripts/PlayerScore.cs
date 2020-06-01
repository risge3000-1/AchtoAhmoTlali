using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int ruinsPlayerHasIteractedWith = 0;
    public int repairedRuins = 0;
    public int destroyedRuins = 0;
    static public int minimalRuinsTointeractWith = 21;
    static public int staticRuinsPlayerHasIteractedWith;

    public delegate void ScoreEvents();
    public static event ScoreEvents HasInteractedWithAllRuins;

    private void Awake()
    {
        staticRuinsPlayerHasIteractedWith = ruinsPlayerHasIteractedWith;
    }

    public void Start()
    {
        //subscribe functions to events
        PlayerMovement.IDestroyedARuin += IncreaseDestroyedRuins;
        PlayerMovement.IrepairedARuin += IncreaseRepairedRuins;
        
    }

    public void IncreaseDestroyedRuins()
    {
        
        ruinsPlayerHasIteractedWith++;
        staticRuinsPlayerHasIteractedWith = ruinsPlayerHasIteractedWith;


        destroyedRuins++;
        CheckTotalScore();
    }

    public void IncreaseRepairedRuins()
    {
        ruinsPlayerHasIteractedWith++;
        staticRuinsPlayerHasIteractedWith = ruinsPlayerHasIteractedWith;

        repairedRuins++;
        CheckTotalScore();
    }

    public void CheckTotalScore()
    { 
        //ruinsPlayerHasIteractedWith++;

        //staticRuinsPlayerHasIteractedWith = ruinsPlayerHasIteractedWith;

        if (ruinsPlayerHasIteractedWith >= minimalRuinsTointeractWith)
        {
            HasInteractedWithAllRuins();
        }
    } 

}
