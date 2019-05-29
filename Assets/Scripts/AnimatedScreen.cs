using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedScreen : Screen
{
    Animator animator = null;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    override public void Open()
    {
        base.Open();
        if (animator)
        {
            animator.SetBool("Show",true);
        }
        

    }

    override public void Close()
    {
        if (animator)
        {
            animator.SetBool("Show",false);
        }
        //base.Close();

    }
}
