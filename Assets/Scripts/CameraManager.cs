using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance{get{return instance;}}
    static CameraManager instance = null;

    CinemachineBrain cinemachineBrain = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        cinemachineBrain = GetComponent<CinemachineBrain>();
        
    }



   

}
