using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnHookTextController : MonoBehaviour
{
    Animator animator = null;
    TextMeshProUGUI tmPro = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tmPro = GetComponent<TextMeshProUGUI>();
    }

    public void ShowText(string text)
    {
        if (tmPro)
        {
            tmPro.text = text;
        }
        if (animator)
        {
            animator.SetTrigger("Show");
        }
        
        
    }
}
