using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class HookGrabber : MonoBehaviour
{
    [Range(0,50)]
    [SerializeField] float _range = 4f;
    [SerializeField] LayerMask _hookLayer;

    [SerializeField] AudioClip grabHookSound;
    [SerializeField] AudioClip unhookSound;

    HookDetector hookDetector = new HookDetector();
    Rigidbody2D _rigidbody = null;
    RopeRenderer ropeRenderer = null;
    Hook _connectedHook = null;
    bool isDashAvailable = false;

   

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        ropeRenderer = GetComponent<RopeRenderer>();
    }

    private void Update()
    {
        if (!GameManager.Instance.InGameLoop)
        {
            //return;
        }

        if (InputController.Instance.IsTapped)
        {
            // first launch jump
           
            //only find nearest hook if not hooked already
            if (_connectedHook==null)
            {
                GrabNearestNonActiveHook();
            }
            

            //Drawing Rope
            if (_connectedHook)
            {
                
                Vector3 hookPos = _connectedHook.transform.position + Vector3.down * 5f;
                ropeRenderer.DrawRope(transform.position, hookPos);
            }
            


        }
        else if (!InputController.Instance.IsTapped)
        {
            if (_connectedHook)
            {
                UnHook();
                //  push grabber when unhooked in direction of velocity
                Vector3 direction = _rigidbody.velocity;
                _rigidbody.AddForce(direction.normalized * Config.UnhookForce,ForceMode2D.Impulse);
                isDashAvailable = true;
                ropeRenderer.ClearRope();
                SoundManager.Instance.PlaySFX(unhookSound);


            }
        }

        // when player is connected to hook this velocity acceerates the swing

        if (_connectedHook)
        {
            AccelerateGrabber();
        }
    }

    private void GrabNearestNonActiveHook()
    {
        Collider2D[] hooks = hookDetector.GetHooksInRadius(transform.position, _range, _hookLayer);
        foreach (Collider2D item in hooks)
        {
            Hook hook = item.GetComponent<Hook>();
            if (hook)
            {
                if (!hook.IsOn)
                {
                    _connectedHook = hook;
                    GrabHook(_connectedHook, _rigidbody);

                    // grab animation
                    Player.Instance.DoGrabAnimation();
                    //grab particles
                    Player.Instance.ShowRopeGrabParticles();
                    //Show OnHook Text
                    string randomText = OnHookTexts.GetRandom();
                    UIManager.Instance.ShowOnHookText(randomText);
                    
                    SoundManager.Instance.PlaySFX(grabHookSound);

                    isDashAvailable = false;
                }
            }
        }
    }

    private void AccelerateGrabber()
    {
        Vector3 direction = _rigidbody.velocity;
        _rigidbody.AddForce(direction.normalized * Config.HookedForce * Time.deltaTime,ForceMode2D.Force);
    }

    private void GrabHook(Hook hook, Rigidbody2D grabber)
    {
        if (hook && grabber)
        {
            hook.SetGrabber(grabber);
        }
    }

    private void UnHook()
    {
        if (_connectedHook)
        {
            _connectedHook.SetGrabber(null);
            _connectedHook = null;
        }
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,_range);
    }




}
