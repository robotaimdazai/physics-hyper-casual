using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance{get{return instance;}}
    static LevelManager instance = null;

    [SerializeField] GameObject levelButtonPrefab = null;
    [SerializeField] Transform levelSelectionPanel = null;
    [SerializeField] TextMeshProUGUI currentLevelText = null;

    Transform activeLevel = null;
    int activeLevelIndex = 0;
    Goal activeGoal = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start() 
    {
        activeGoal = Goal.GetActiveGoal();
        CreateLevelButtons();
        //Activate Level 1 at start
        ActivateLevel(1);
    }
    
    void CreateLevelButtons()
    {
        if (!levelButtonPrefab|| !levelSelectionPanel)
        {
            return;
        }
        int levelNumber = 1;
        foreach(Transform items in transform)
        {
            GameObject levelButtonObject = Instantiate(levelButtonPrefab,levelSelectionPanel);
            LevelButton levelButton = levelButtonObject.GetComponent<LevelButton>();
            if (levelButton)
            {
                levelButton.SetLevelNumber(levelNumber);
                
            }
            levelNumber++;
        }
    }

    public void ActivateLevel(int level)
    {
        //subtracting 1 because child index start from 0
        activeLevelIndex = level;
        int levelToActivateInChild  = level - 1;
        
        if (levelToActivateInChild<0)
        {
            return;
        }
        for(int i = 0;i<transform.childCount;i++)
        {
            Transform currentLevel = transform.GetChild(i);
            if (i==levelToActivateInChild)
            {
                activeLevel = currentLevel;
                currentLevel.gameObject.SetActive(true);
                if (currentLevelText)
                {
                    currentLevelText.text = "Level "+ level;
                }
            }
            else
            {
                currentLevel.gameObject.SetActive(false);
            }
        }

        SetPlayerToStartPosition();
        activeGoal = Goal.GetActiveGoal();
        LevelProgressController.Instance.RecalculateGoalDistance();
    }

    private void SetPlayerToStartPosition()
    {
        Transform startPlatform = GetActiveStartPlatform();
        Player.Instance.transform.position = startPlatform.position + Vector3.up * 1.2f;
        Player.Instance.transform.rotation = Quaternion.identity;
    }

//------------Helper
    Transform GetActiveStartPlatform()
    {
        Transform platform = null;
         StartPlatform startPlatform = FindObjectOfType<StartPlatform>();
         if (startPlatform)
         {
             platform = startPlatform.transform;
         }
        return platform;
    }

    public void RetryLevel()
    {
        //turning off and on enables all hooks by onEnable Function
        activeLevel.gameObject.SetActive(false);
        activeLevel.gameObject.SetActive(true);
        GameManager.Instance.InGameLoop = true;
        SetPlayerToStartPosition();
        UIManager.Instance.OpenHomeScreen();
        activeGoal.ResetLevelStatus();
        activeGoal.TurnOffCamera();
        Player.Instance.DeathCameraUnFollowPlayer();
        Player.Instance.TurnOnCamera();
    }

    public void LoadNextLevel()
    {
        int nextLevel = activeLevelIndex + 1;
        if (nextLevel>transform.childCount)
        {
            Debug.Log("No More Levels");
            return;
        }
        GameManager.Instance.InGameLoop = true;
        ActivateLevel(nextLevel);
        UIManager.Instance.OpenHomeScreen();
        activeGoal.ResetLevelStatus();
        activeGoal.TurnOffCamera();
        Player.Instance.DeathCameraUnFollowPlayer();
        Player.Instance.TurnOnCamera();
    }

   
}
