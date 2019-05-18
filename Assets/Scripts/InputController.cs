using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get { return _instance; } }
    private static InputController _instance = null;


    public bool IsTapped { get { return _tap; } }
    private bool _tap = false;


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance!=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            _tap = true;
        }

        if (Input.GetMouseButton(0))
        {
            return;
        }

        _tap = false; 
    }


}
