using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    // Start is called before the first frame update
   

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    public virtual void Open()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

    }

    public virtual void Close()
    {
        gameObject.SetActive(false);

    }
}
