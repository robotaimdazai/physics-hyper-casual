using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Goal : MonoBehaviour
{
    public static Goal Instance{get{return instance;}}
    private static Goal instance = null;

    [SerializeField]float timeCheck = 1.5f;
    [SerializeField] float radius = 2f;
    [SerializeField] Vector3 offset = new Vector3(2.5f,0f,0f);
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] CinemachineVirtualCamera goalCamera =null;

    float timer = 0f;
    bool levelPassed = false;
   
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
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

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        // goal pivot point is not in centre, this offset balances it
        Gizmos.DrawWireSphere(transform.position + offset,radius);
    }
   
   
   
}
