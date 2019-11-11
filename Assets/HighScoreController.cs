using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreController : MonoBehaviour
{
   
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoretext;
    [SerializeField] TextMeshProUGUI newHighScoreText;
    [SerializeField] TextMeshProUGUI failScreenHighScoreText;

    float savedHighScore = 0f;
    float currentScore = 0f;
    string highScoreKey = "HighScore";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            savedHighScore = PlayerPrefs.GetFloat(highScoreKey);
        }
        highScoretext.text = string.Format("{0:0}", savedHighScore);
    }

    public void SetScore(float score)
    {
        currentScore = score;

        if (currentScoreText)
        {
            currentScoreText.text = string.Format("{0:0}", currentScore); 
        }

        if (score > savedHighScore)
        {
            
            highScoretext.text = string.Format("{0:0}", currentScore);
        }
    }

    public void ResetText()
    {
        if (currentScoreText)
        {
            currentScoreText.text = string.Format("{0:0}", 0f);
        }
    }

    public void ShowFailScreenText()
    {
        if (currentScore > savedHighScore)
        {
            newHighScoreText.transform.gameObject.SetActive(true);
            savedHighScore = currentScore;
        }
        else
        {
            newHighScoreText.transform.gameObject.SetActive(false);
        }
        
        failScreenHighScoreText.text = string.Format("{0:0}", currentScore);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PlayerPrefs.SetFloat(highScoreKey, savedHighScore);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat(highScoreKey, savedHighScore);
    }



}
