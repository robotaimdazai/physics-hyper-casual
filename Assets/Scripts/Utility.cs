using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
   public static Sprite LoadSprite(string name)
   {
       Sprite ret = null;
       ret = Resources.Load<Sprite>("Sprites/"+name);
       return ret;
   }
}
