
using UnityEngine;

public class OnHookTexts
{
   static string[] onHookTexts ={"Smooth","Perfect","Good","Wohoo","Whoa","Nice"};

   public static string GetRandom()
   {
        string  ret = "";
        if (onHookTexts.Length>0)
        {
            int randomNumber = Random.Range(0,onHookTexts.Length);
            ret = onHookTexts[randomNumber];
        }
        
        return ret;
   }
}
