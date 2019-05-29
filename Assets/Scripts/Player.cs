using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem ropeGrabParticle = null;
    public static Player Instance{get{return instance;}}

    [SerializeField]CinemachineVirtualCamera playerCamera = null;
    [SerializeField] CinemachineVirtualCamera deathCamera = null;

    [Header("Sounds")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip hitSound = null;
    

    static Player instance = null;
    Animator animator = null;
    HookGrabber hookGrabber = null;
    Rigidbody2D rigidbody2d = null;
    ParticleController particleController = null;
    


  
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
        particleController = GetComponent<ParticleController>();
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
        Vector3 offset = Vector3.down * 0.5f;
        particleController.ShowParticle(0,transform.position + offset,Quaternion.identity);
        DoJumpAnimation();
        SoundManager.Instance.PlaySFX(jumpSound);

    }

    public void TurnOnCamera()
    {
        if (playerCamera)
        {
            playerCamera.gameObject.SetActive(true);
        }
    }

    public void TurnOffCamera()
    {
        if (playerCamera)
        {
            playerCamera.gameObject.SetActive(false);
        }
    }

    public void SetDeathCameraPosition()
    {
        deathCamera.transform.position = new Vector3(transform.position.x,deathCamera.transform.position.y,
        deathCamera.transform.position.z);
    }

    public void DeathCameraFollowPlayer()
    {
        deathCamera.Follow = transform;
        deathCamera.LookAt = transform;
    }

    public void DeathCameraUnFollowPlayer()
    {
        deathCamera.Follow = null;
        deathCamera.LookAt = null;
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Vector2 offset = Vector3.right * 0.5f;
        particleController.ShowParticle(0,collision.contacts[0].point + offset,Quaternion.identity);
        if (collision.gameObject.GetComponent<StartPlatform>()==null)
        {
            SoundManager.Instance.PlaySFX(hitSound);
        }
        
    }

    public void FreezePlayer()
    {
        rigidbody2d.velocity = Vector3.zero;
    }

   
}
