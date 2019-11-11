using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class Testing:MonoBehaviour
{
    [MenuItem("Testing/Reset Player Pos")]
    public  static void ResetPlayerPosition()
    {
        StartPlatform startPlatform =FindObjectOfType<StartPlatform>();
        Player player = FindObjectOfType<Player>();
        if (startPlatform && player)
        {
            Vector3 offset = Vector3.up * 1.2f;
            player.transform.position = startPlatform.transform.position + offset;
        }
        
    }

    [MenuItem("Testing/Delete all saved data")]
    public static void DeleteAllSavedData()
    {
        if (File.Exists(Application.persistentDataPath + DataSaver.LevelDataSavePath))
        {
            File.Delete(Application.persistentDataPath + DataSaver.LevelDataSavePath);
            Debug.Log("Levels data deleted");
        }
        if (File.Exists(Application.persistentDataPath + DataSaver.LastLevelPlayedSavePath))
        {
            File.Delete(Application.persistentDataPath + DataSaver.LastLevelPlayedSavePath);
            Debug.Log("last level data deleted");
        }
        if (File.Exists(Application.persistentDataPath + DataSaver.CrownCountSavePath))
        {
            File.Delete(Application.persistentDataPath + DataSaver.CrownCountSavePath);
            Debug.Log("Crown data deleted");
        }
        if (File.Exists(Application.persistentDataPath + DataSaver.CharacterDataPath))
        {
            File.Delete(Application.persistentDataPath + DataSaver.CharacterDataPath);
            Debug.Log("Characters data deleted");
        }

    }

    [MenuItem("Testing/Reset HighScore")]
    public static void ResetHighScore()
    {
        string highScoreKey = "HighScore";
        PlayerPrefs.DeleteKey(highScoreKey);
      
    }
}
