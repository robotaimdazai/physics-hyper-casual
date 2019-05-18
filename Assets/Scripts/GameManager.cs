using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public  readonly static string HomeScene ="HomeScene";
    public static GameManager Instance{get{return instance;}}
    private static GameManager instance = null;

    [HideInInspector] public bool InGameLoop = true;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
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

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
        InGameLoop = true;
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while(!async.isDone)
        {
            yield return null;
        }
    }

   

}
