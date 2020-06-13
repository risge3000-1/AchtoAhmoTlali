using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue
{
    public string PlayerName;
    public string AIname = "AI";

    [TextArea(5, 10)]
    
    public string[] sentences;

   
}
