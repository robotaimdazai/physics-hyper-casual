using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeRenderer : MonoBehaviour
{
    LineRenderer _lineRenderer = null;


    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawRope(Vector3 startPos, Vector3 endPos)
    {
        if (!_lineRenderer)
        {
            return;
        }

        
        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1,endPos);
    }

    public void ClearRope()
    {
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.zero);
    }
}
