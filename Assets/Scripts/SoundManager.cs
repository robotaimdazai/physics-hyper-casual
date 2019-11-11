using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Sprite MuteSprite;
    public Sprite UnMuteSprite;
    public Button SoundButton;

    public static SoundManager Instance{get{return instance;}}
    static SoundManager instance= null;
    AudioSource audioSource = null;
    bool isMuted = false;

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
        if (SoundButton)
        {
            if (mute)
            {
                if(MuteSprite)
                SoundButton.image.sprite = MuteSprite;
            }
            else
            {
                if (UnMuteSprite)
                    SoundButton.image.sprite = UnMuteSprite;
            }
            
        }
   }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        Mute(isMuted);
    }
}
