using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrownManager : MonoBehaviour
{
    static CrownManager instance = null;

    public static CrownManager Instance
    {
        get{return instance;}
    }

    public int CrownCount = 0;

    [SerializeField] TextMeshProUGUI crownCountText = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //returns negative when no saved crown count is found
        int loadedCrownCount = DataSaver.LoadCrownCount();
        if (loadedCrownCount>=0)
        {
            CrownCount = loadedCrownCount;
        }
    }

    private void Update() 
    {
        if (crownCountText)
        {
            crownCountText.text = CrownCount.ToString();
        }
        
    }

    void OnApplicationQuit()
    {
        DataSaver.SaveCrownCount(CrownCount);
    }

    private void OnApplicationPause(bool pauseStatus) 
    {
        if (pauseStatus)
        {
            DataSaver.SaveCrownCount(CrownCount);
        }
    }
}
