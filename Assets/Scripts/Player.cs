using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem ropeGrabParticle = null;

    public static Player Instance{get{return instance;}}

    static Player instance = null;
    Animator animator = null;
    HookGrabber hookGrabber = null;
    Rigidbody2D rigidbody2d = null;
  
    // Start is called before the first frame update

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else if (instance !=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        hookGrabber = GetComponent<HookGrabber>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }


    public void DoJumpAnimation()
    {
        if (!animator)
        {
            return;
        }

        animator.SetTrigger("Jump");

    }

    public void DoGrabAnimation()
    {
        if (!animator)
        {
            return;
        }

        animator.SetTrigger("Grab");
    }

    public void ShowRopeGrabParticles()
    {
        if (!ropeGrabParticle)
        {
            return;
        }
        ropeGrabParticle.Play();

    }

    public void DoStartJump()
    {
        if (rigidbody2d)
        {
            rigidbody2d.AddForce(Config.StartJumpVector * Config.StartJumpForce,ForceMode2D.Impulse);
        }
        DoJumpAnimation();

    }

   
   
}
