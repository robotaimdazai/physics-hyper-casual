using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance{get{return instance;}}
    static SoundManager instance= null;
    AudioSource audioSource = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if(instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

   public void PlaySFX(AudioClip clip)
   {
       if (audioSource && clip)
       {
           audioSource.PlayOneShot(clip);
       }
       
   }

   public void Mute(bool mute)
   {
       audioSource.mute = mute;
   }
}
