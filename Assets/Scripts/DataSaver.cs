using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
 using System.Runtime.Serialization.Formatters.Binary;


public class DataSaver
{
    public static readonly string LevelDataSavePath = "LevelDataSavePath.Dat";
    public static readonly string LastLevelPlayedSavePath = "LastLevelSavePAth.Dat";
    public static readonly string CrownCountSavePath = "CrownCountSavePath.Dat";

    public static void SaveLevelData(LevelData[] levelDataArray)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + LevelDataSavePath);
        bf.Serialize(file,levelDataArray);
        file.Close();
    }

    public static LevelData[] LoadLevelData()
    {
        LevelData[] ret = null;
        
        if (File.Exists(Application.persistentDataPath+ LevelDataSavePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + LevelDataSavePath, FileMode.Open);
            ret = (LevelData[])bf.Deserialize(file);
            file.Close();
        }
        
        return ret;
    }

    public static void SaveLastLevelPlayed(int level)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + LastLevelPlayedSavePath);
        bf.Serialize(file,level);
        file.Close();
    }

    public static int LoadLastLevelPlayed()
    {
         int ret = -1;
        
        if (File.Exists(Application.persistentDataPath+ LastLevelPlayedSavePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + LastLevelPlayedSavePath, FileMode.Open);
            ret = (int)bf.Deserialize(file);
            file.Close();
        }
        
        return ret;
    }

    public static void SaveCrownCount(int level)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + CrownCountSavePath);
        bf.Serialize(file,level);
        file.Close();
    }

    public static int LoadCrownCount()
    {
         int ret = -1;
        
        if (File.Exists(Application.persistentDataPath+ CrownCountSavePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + CrownCountSavePath, FileMode.Open);
            ret = (int)bf.Deserialize(file);
            file.Close();
        }
        
        return ret;
    }

}

