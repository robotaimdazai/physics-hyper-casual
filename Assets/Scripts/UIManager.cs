using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance{get{return instance;}}
    [SerializeField] Screen homeScreen = null;
    [SerializeField] Screen gameScreen = null;
    [SerializeField] Screen failScreen = null;
    [SerializeField] Screen passScreen = null;
    [SerializeField] Screen levelScreen = null;

    [SerializeField]Transform[] elementsToHide;

    private static UIManager instance = null;
    List<Screen> screens = new List<Screen>();
    private Screen currentScreen = null;
    private Screen previousScreen  = null;
    [SerializeField]OnHookTextController onHookController = null;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        //singleton
        if (instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitAllScreens();
        CloseAllScreens();
        OpenHomeScreen();

    }

    private void InitAllScreens()
    {
        foreach(Transform item in transform)
        {
            Screen currentScreen = item.GetComponent<Screen>();
            if (currentScreen)
            {
                screens.Add(currentScreen);
            }
             
        }   
    }

    private void CloseAllScreens()
    {
        foreach(Screen screen in screens)
        {
            screen.Close();

            //reset position also
            ResetScreenPosition(screen);
        }     

    }

    public void OpenHomeScreen()
    {
        SwitchScreen(homeScreen);
    }

    public void OpenGameScreen()
    {
        SwitchScreen(gameScreen);
    }

    public void OpenFailScreen()
    {
        SwitchScreen(failScreen);
    }

    public void OpenPassScreen()
    {
        SwitchScreen(passScreen);
    }

    public void OpenLevelScreen()
    {
        SwitchScreen(levelScreen);
    }

    public void HideElements()
    {
        foreach(Transform item in elementsToHide)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void ShowElements()
    {
         foreach(Transform item in elementsToHide)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void ShowOnHookText(string text)
    {
        if (onHookController)
        {
            onHookController.ShowText(text);
        }
    }


    /// Helper functions
    private void SwitchScreen(Screen screen)
    {
        if (!screen)
        {
            return;

        }    

        if (currentScreen==null)
        {
            previousScreen = screen;
        }
        else
        {
            previousScreen = currentScreen;
            previousScreen.Close();
        }
        
        currentScreen = screen;
        currentScreen.Open();

    }

    void ResetScreenPosition(Screen screen)
    {
        screen.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }


    


}
