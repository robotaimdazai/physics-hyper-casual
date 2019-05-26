using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField]GameObject[] particles = null;

    public void ShowParticle(int index,Vector2 position, Quaternion rotation)
    {
        if (index<particles.Length && index>= 0)
        {
            GameObject particle = Instantiate(particles[index],position,rotation);
            ParticleSystem ps = particle.GetComponent<ParticleSystem>();
            Destroy(particle,ps.main.duration);
        }
    }
    
}
