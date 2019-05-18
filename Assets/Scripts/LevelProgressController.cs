using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressController : MonoBehaviour
{
    [SerializeField]private Slider slider = null;
    [SerializeField] float accuracy = 0.5f;

    float totalDistance = 0f;

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

        if (slider)
        {
            slider.value  = normalizedSliderValue;
        }
    }
}
