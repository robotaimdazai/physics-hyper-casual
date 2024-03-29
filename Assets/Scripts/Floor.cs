﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Floor : MonoBehaviour
{
    HighScoreController highscoreController;

    private void Awake()
    {
        highscoreController = FindObjectOfType<HighScoreController>();
    }

    [SerializeField] AudioClip failSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.InGameLoop)
        {
            //TODO End Game  Sequence here
            GameManager.Instance.InGameLoop = false;
            Player.Instance.SetDeathCameraPosition();
            Player.Instance.DeathCameraFollowPlayer();
            Player.Instance.TurnOffCamera();
            AdManager.Instance.ShowAd();
            UIManager.Instance.OpenFailScreen();
            highscoreController.ShowFailScreenText();


        }
        SoundManager.Instance.PlaySFX(failSound);
    }
}
