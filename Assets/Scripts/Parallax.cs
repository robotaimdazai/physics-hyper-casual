using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] GameObject _camera = null;
    [SerializeField] float _parallaxAmount = 1f;

    float _length, _startPos;
    float offset = 25; //  see ahead offset
    

    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x/2;
    }

    private void FixedUpdate()
    {
        float temp = _camera.transform.position.x * (1 - _parallaxAmount);

        float distance = (_camera.transform.position.x * _parallaxAmount);
        transform.position = new Vector3(_startPos + distance,transform.position.y,transform.position.z);

        if (temp > _startPos + _length - offset)
        {
            _startPos += _length;
        }
        else if(temp < _startPos - _length)
        {
            _startPos -= _length;
        }
    }
}
