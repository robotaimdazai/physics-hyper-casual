using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class JumpButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnJump;
    public void OnPointerClick(PointerEventData eventData)
    {
        Player.Instance.DoStartJump();
        UIManager.Instance.OpenGameScreen();
        StartPlatform startPlatform = LevelManager.Instance.ActiveStartPlatform;
        if (startPlatform)
        {
            startPlatform.DisableCollisionWithPlayer();
        }
        if (OnJump!=null)
        {
            OnJump.Invoke();
        }
    }

    
}
