using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class Hook : MonoBehaviour
{
    public bool IsOn { get; set; }

    HingeJoint2D _hinge = null;

    public void SetGrabber(Rigidbody2D grabber)
    {

         IsOn = true;
        _hinge.connectedBody = grabber;
        
    }

    private void Awake()
    {
        _hinge = GetComponent<HingeJoint2D>();
    }
    private void OnEnable() 
    {
        IsOn = false;
    }
}
