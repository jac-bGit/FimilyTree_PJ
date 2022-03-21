using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreensManager : MonoBehaviour
{
    public UIScreen[] m_aScreens;
    public SituationManagerBehavior m_SitManager;

     //------------------------------------------------------------------
    private void Start() 
    {
        SetActiveScreen("Story");
        m_SitManager.NextSituation("Start");
    }

    //------------------------------------------------------------------
    public void SetActiveScreen(string tag)
    {
        foreach(UIScreen screen in m_aScreens)
        {
            if(screen.m_sTag == tag)
            {
                screen.m_GOScreen.SetActive(true);
                continue;
            }

            screen.m_GOScreen.SetActive(false);
        }
    }

    public void ActiveScreen(string tag, bool activate)
    {
        foreach(UIScreen screen in m_aScreens)
        {
            if(screen.m_sTag == tag)
            {
                screen.m_GOScreen.SetActive(activate);
                break;
            }
        }
    }
}

//------------------------------------------------------------------
[Serializable]
public class UIScreen
{
    public string m_sTag;
    public GameObject m_GOScreen;
}
