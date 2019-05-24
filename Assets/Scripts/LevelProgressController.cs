using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressController : MonoBehaviour
{
    public static LevelProgressController Instance{get{return instance;}}
    static LevelProgressController instance = null;

    [SerializeField] RectTransform fill = null;

    float totalDistance = 0f;
    float fillStartPosX=-570f;
    float fillEndPosX =-24f;

    Goal activeGoal = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        RecalculateGoalDistance();
    }

    public void RecalculateGoalDistance()
    {
        activeGoal = Goal.GetActiveGoal();
        totalDistance = activeGoal.transform.position.x - Player.Instance.transform.position.x;
    }


    // Update is called once per frame
    void LateUpdate()
    {
       
        float playerX = Player.Instance.transform.position.x;
        float goalX = activeGoal.transform.position.x;

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
