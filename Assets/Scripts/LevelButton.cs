using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public bool IsLocked
    {
        get{return isLocked;}
        set
        {
            isLocked = value;
            if (isLocked)
            {
                button.image.sprite = levelLockedSprite;
                levelNumberText.color = levelLockedColor;
                button.interactable = false;
            }
            else
            {
                button.image.sprite = leveUnlockedSprite;
                levelNumberText.color = Color.white;
                button.interactable = true;
            }
        }
    }

    public bool IsClear
    {
        get{return isClear;}
        set
        {
            isClear = value;
            if (isClear)
            {
                button.image.sprite = levelClearSprite;
                levelNumberText.color = levelClearColor;
            }
        }
    }

    public bool HasCrown
    {
        get{return hasCrown;}
        set
        {
            hasCrown = value;
            if (hasCrown)
            {
                button.image.sprite = levelClearCrownSprite;
                levelNumberText.color = levelClearColor;
            }
        }
    }


    [SerializeField] TextMeshProUGUI levelNumberText = null;
    [SerializeField] Sprite levelLockedSprite = null;
    [SerializeField] Sprite leveUnlockedSprite = null;
    
    [SerializeField] Sprite levelClearSprite = null;
    [SerializeField] Sprite levelClearCrownSprite = null;
    [SerializeField] Color levelLockedColor;    
    [SerializeField] Color levelClearColor;

    Button button = null;
    private int levelNumber = 0;

    bool isLocked = false;
    bool isClear = false;
    bool hasCrown = false;

    private void Awake() 
    {
        button = GetComponent<Button>();
    }

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

    public void OpenHomeScreen()
    {
        UIManager.Instance.OpenHomeScreen();
    }
    
}
