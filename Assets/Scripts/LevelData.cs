using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData 
{
   
    public bool Islocked;
    public bool HasCrown;
    public bool IsClear;

    public LevelData( bool isLocked, bool hasCrown, bool isClear)
    {
        Islocked = isLocked;
        HasCrown = hasCrown;
        IsClear = isClear;
    }
}

