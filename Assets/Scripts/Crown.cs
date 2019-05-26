using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crown : MonoBehaviour
{
    [SerializeField] GameObject crownPickParticle = null;
    public UnityEvent OnCrownPicked;

    private void Start() 
    {
        if (LevelManager.Instance.HasCrownForCurrentLevel())
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Instantiate(crownPickParticle,transform.position,Quaternion.identity);
            if (OnCrownPicked!=null)
            {
                OnCrownPicked.Invoke();
            }

        }
    }
}
