using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
  public void DisableCollisionWithPlayer()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    public void EnableCollisionWithPlayer()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }
}
