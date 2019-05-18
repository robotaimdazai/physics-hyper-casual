using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Fly controls")]
    [Range(0,20)]
    [SerializeField] float waveAmplitude = 1f;
    [Range(0, 20)]
    [SerializeField] float waveFrequency = 1f;
    [SerializeField] float flySpeed = 1f;
    



    float timer = 1f;

    void Update()
    {
        Fly();
    }

    private void Fly()
    {
        timer += Time.deltaTime;
        //creating sine wave
        float sineFactor = waveAmplitude * Time.deltaTime* Mathf.Sin(timer * waveFrequency);
        // move vector sinefactor on y axis and move speed on x axis
        Vector3 moveVector = new Vector3(flySpeed * Time.deltaTime, sineFactor, 0f);
        transform.position = transform.position + moveVector;
    }

   
}
