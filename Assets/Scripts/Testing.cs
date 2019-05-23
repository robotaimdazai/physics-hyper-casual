using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Testing:MonoBehaviour
{
    [MenuItem("Testing/Reset Player Pos")]
    public  static void ResetPlayerPosition()
    {
        StartPlatform startPlatform =FindObjectOfType<StartPlatform>();
        Player player = FindObjectOfType<Player>();
        if (startPlatform && player)
        {
            Vector3 offset = Vector3.up * 1.2f;
            player.transform.position = startPlatform.transform.position + offset;
        }
        
    }
}
