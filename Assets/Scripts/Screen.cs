using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    // Start is called before the first frame update

    public void Open()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

    }

    public void Close()
    {
        gameObject.SetActive(false);

    }
}
