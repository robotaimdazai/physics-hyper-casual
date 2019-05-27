using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Floor : MonoBehaviour
{
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
            UIManager.Instance.OpenFailScreen();
            SoundManager.Instance.PlaySFX(failSound);
           
        }
    }
}
