using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public List<Situation> m_aSituations = new List<Situation>();

    //------------------------------------------------------------------
    private void Awake() 
    {
        // Load situation
        InsertSituation("BlindMan");
        InsertSituation("Continue");
        InsertSituation("Ending");
        InsertSituation("HeavyDoor");
        InsertSituation("Intro");
        InsertSituation("Pendulum");
        //InsertSituation("Spikes");
        InsertSituation("Start");
        InsertSituation("StrongMan");
        InsertSituation("Wizard");

        foreach(Situation sit in m_aSituations)
        {
            if(sit != null)
                sit.m_iIsSolvedWith = -1;
        }
    }

    private void InsertSituation(string path)
    {
        Situation situation = Resources.Load<Situation>("SituationsSO/" + path);
        m_aSituations.Add(situation);
    }

    //------------------------------------------------------------------
    public Situation GetSituationByTag(string tag)
    {
        foreach(Situation situation in m_aSituations)
        {
            if(situation == null)
                continue;

            Debug.Log("situation text: " + situation.m_sTag);
            if(situation.m_sTag == tag)
            {
                Debug.Log("situation passed");
                return situation;
            }
        }

        Debug.Log("situation fail");
        return null;
    }
}
