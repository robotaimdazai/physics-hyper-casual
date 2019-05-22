using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressController : MonoBehaviour
{
    [SerializeField] RectTransform fill = null;

    float totalDistance = 0f;
    float fillStartPosX=-570f;
    float fillEndPosX =-24f;

    // Start is called before the first frame update
    void Start()
    {
        totalDistance = Goal.Instance.transform.position.x - Player.Instance.transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
        float playerX = Player.Instance.transform.position.x;
        float goalX = Goal.Instance.transform.position.x;

        if (playerX<0)
        {
            playerX = 0f;
        }

        float remainingDistance = goalX - playerX; 

        if (remainingDistance<0)
        {
            remainingDistance = 0;
        }

        float normalizedSliderValue = 1- (remainingDistance/totalDistance);

        if (fill)
        {
            SetValue(normalizedSliderValue);
        }
    }

    private void SetValue(float value)
    {
        float fillNewPosX = Mathf.Lerp(fillStartPosX,fillEndPosX,value);
        fill.localPosition = new Vector3(fillNewPosX,0f,0f);
    }


}
