using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Goal : MonoBehaviour
{

    [SerializeField]float timeCheck = 1.5f;
    [SerializeField] float radius = 2f;
    [SerializeField] Vector3 offset = new Vector3(2.5f,0f,0f);
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] CinemachineVirtualCamera goalCamera =null;

    [SerializeField] GameObject goalParticles = null;


    float timer = 0f;
    bool levelPassed = false;
    bool crownPicked = false;
   
    private void OnEnable() 
    {
        goalCamera.gameObject.SetActive(false);
    }

    private void Update() 
    {
        bool playerReached = HasPlayerReachedGoal();
        if (playerReached && !levelPassed)
        {
            timer+=Time.deltaTime;
            if (timer>=timeCheck)
            {
                UIManager.Instance.OpenPassScreen();
                levelPassed = true;
                goalCamera.gameObject.SetActive(true);
                Player.Instance.TurnOffCamera();
                if (LevelManager.Instance.HasCrownForCurrentLevel())
                {
                    crownPicked = true;
                }
                LevelManager.Instance.SetCurrentLevelClear(crownPicked);
                SpawnGoalParticles();
            }
        }
        else
        {
            timer = 0f;
        }
    }

    bool HasPlayerReachedGoal()
    {
        bool ret = false;
        if (Physics2D.OverlapCircle(transform.position + offset,radius,playerLayerMask))
        {
            ret = true;
        }
        
        return ret;
    }

    public void ResetLevelStatus()
    {
        levelPassed = false;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        // goal pivot point is not in centre, this offset balances it
        Gizmos.DrawWireSphere(transform.position + offset,radius);
    }

    public static Goal GetActiveGoal()
    {
        Goal activeGoal = null;
        activeGoal = FindObjectOfType<Goal>();
        return activeGoal;
    }

    public void TurnOffCamera()
    {
        goalCamera.gameObject.SetActive(false);
    }

   private void SpawnGoalParticles()
   {
       Instantiate(goalParticles,transform.position + offset,Quaternion.identity);
   }

   public void CownPicked()
   {
       crownPicked = true;
   }
   
   
}
