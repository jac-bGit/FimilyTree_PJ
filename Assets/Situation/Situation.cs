using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "New Situation", menuName = "Situation")]
public class Situation : ScriptableObject
{
    [SerializeField] public string m_sTag; // Tag name
    [TextAreaAttribute(5, 20)]
    [SerializeField] public string m_sDesription;
    [SerializeField] public GameObject m_GOIllustration; 
    [SerializeField] public bool m_bJustContinue = false;
    [SerializeField] public int m_iIsSolvedWith = -1;
    [SerializeField] public Option[] m_aOptions; 
    
}


