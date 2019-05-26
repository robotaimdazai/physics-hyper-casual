using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance{get{return instance;}}
    static LevelManager instance = null;

    public StartPlatform ActiveStartPlatform{ get; private set;}

    [SerializeField] bool testMode = true;
    [SerializeField] GameObject levelButtonPrefab = null;
    [SerializeField] Transform levelSelectionPanel = null;
    [SerializeField] TextMeshProUGUI currentLevelText = null;
    [SerializeField] TextMeshProUGUI crownsAchievedText = null;

    [Header("Level")]
    public LevelData[] levels;


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

        Init();
    }


    public void Init()
    {
        LevelData[] loadedLevelData = DataSaver.LoadLevelData();
        if (loadedLevelData!=null)
        {
            levels = loadedLevelData;
        }
        int levelToLoad = 1;
        int lastPlayedLevelSaved = DataSaver.LoadLastLevelPlayed();
        if (lastPlayedLevelSaved>0)
        {
            levelToLoad = lastPlayedLevelSaved;
        }
        CreateLevelButtons();
        //Activate Level 1 at start
        ActivateLevel(levelToLoad);
        activeGoal = Goal.GetActiveGoal();
        UpdateCrownsAchieved();
    }
    
    void CreateLevelButtons()
    {
        if (!levelButtonPrefab|| !levelSelectionPanel)
        {
            return;
        }

        int levelNumber = 1;
        int index = 0; //this is used for transferring data from levels array to buttons
        foreach(Transform items in transform)
        {
            GameObject levelButtonObject = Instantiate(levelButtonPrefab,levelSelectionPanel);
            LevelButton levelButton = levelButtonObject.GetComponent<LevelButton>();
            if (levelButton)
            {
                levelButton.SetLevelNumber(levelNumber);
                if(levels.Length != transform.childCount)
                {
                    Debug.Log("Levels array and level objects are not consistent");
                }
                if (!testMode)
                {
                    levelButton.IsLocked = levels[index].Islocked;  
                }     
                levelButton.IsClear = levels[index].IsClear;
                levelButton.HasCrown = levels[index].HasCrown;
            }
            levelNumber++;
            index++;
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
        StartPlatform startPlatform = GetActiveStartPlatform();
        ActiveStartPlatform = startPlatform;
        startPlatform.EnableCollisionWithPlayer();
        Player.Instance.transform.position = startPlatform.transform.position + Vector3.up * 1.2f;
        Player.Instance.transform.rotation = Quaternion.identity;
    }

//------------Helper
    StartPlatform GetActiveStartPlatform()
    {
        StartPlatform platform = null;
         StartPlatform startPlatform = FindObjectOfType<StartPlatform>();
         if (startPlatform)
         {
             platform = startPlatform;
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

    public void SetCurrentLevelClear(bool hasCrown)
    {
       
        int currentLevelIndexInArray = activeLevelIndex - 1;

        if (currentLevelIndexInArray >= transform.childCount)
        {
            Debug.Log("No More Levels");
            return;
        }

        levels[currentLevelIndexInArray].IsClear = true;
       
        levels[currentLevelIndexInArray].HasCrown = hasCrown;
        
        // getting level Button for cleared Level
        LevelButton levelButton = levelSelectionPanel.GetChild(currentLevelIndexInArray).GetComponent<LevelButton>();
        levelButton.IsClear = true;
        
        levelButton.HasCrown = hasCrown;
        
        //unlocking nextLevel
        int nextLevel = currentLevelIndexInArray+1;
        if (nextLevel<levels.Length && !levels[nextLevel].IsClear)
        {
             levels[nextLevel].Islocked = false;
            LevelButton nextLevelButton = levelSelectionPanel.GetChild(nextLevel).GetComponent<LevelButton>();
            nextLevelButton.IsLocked = false;
        }
       
       //counting crowns achieved again
        UpdateCrownsAchieved();

    }
    public  /// <summary>
    /// Callback sent to all game objects before the application is quit.
    /// </summary>
    void OnApplicationQuit()
    {
        DataSaver.SaveLevelData(levels);
        DataSaver.SaveLastLevelPlayed(activeLevelIndex);
    }
    /// <summary>
    /// Callback sent to all game objects when the player pauses.
    /// </summary>
    /// <param name="pauseStatus">The pause state of the application.</param>
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            DataSaver.SaveLevelData(levels);
            DataSaver.SaveLastLevelPlayed(activeLevelIndex);
        }
    }

    private void UpdateCrownsAchieved()
    {
        int crownsAchieved = 0;
        foreach (Transform item in levelSelectionPanel)
        {
            LevelButton levelButton = item.GetComponent<LevelButton>();
            if (levelButton)
            {
                if (levelButton.HasCrown)
                {
                    crownsAchieved++;
                }
            }
        }
        if (crownsAchievedText)
        {
            crownsAchievedText.text = crownsAchieved + " / " + levelSelectionPanel.childCount; 
        }
    }

    public bool HasCrownForCurrentLevel()
    {
        bool ret = false;
        if (levels[activeLevelIndex-1].HasCrown)
        {
            ret = true;
        }

        return ret;
    }

   
}
