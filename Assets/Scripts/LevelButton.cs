using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelNumberText = null;

    private int levelNumber = 0;

    public void SetLevelNumber(int number)
    {
        if (levelNumberText)
        {
            levelNumberText.text = number.ToString();
        }
        levelNumber = number;
    }

    public void ActivateLevel()
    {
        if (levelNumber==0)
        {
            return;
        }

        LevelManager.Instance.ActivateLevel(levelNumber);
    }
    
}
