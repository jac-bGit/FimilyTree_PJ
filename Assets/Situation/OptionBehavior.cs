using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionBehavior : MonoBehaviour
{
    [SerializeField] private Option m_Option;

    // Components
    private Button m_Button;

    public delegate void ClickOption(Option option);
    public event ClickOption m_OnClicked;

    //------------------------------------------------------------------
    private void OnClickedInvoke(Option option)
    {
        if(m_OnClicked != null)
            m_OnClicked(option);
    }

    //------------------------------------------------------------------
    void Awake()
    {
        // Get components 
        m_Button = GetComponent<Button>();

        m_Button.onClick.AddListener(OnClick);

        //Debug.Log("create beh");
        m_Option = null;
    }

    //------------------------------------------------------------------
    public void SetOption(Option option)
    {
        m_Option = option;

        if(m_Button == null)
            m_Button = GetComponent<Button>();

        //Debug.Log("add option: " + option.m_sDescription);

        // Set text 
        Text butText = m_Button.GetComponentInChildren<Text>();
        butText.text = m_Option.m_sDescription;
    }

    //------------------------------------------------------------------
    void OnClick()
    {
        OnClickedInvoke(m_Option);
    }


}

//------------------------------------------------------------------
// Custom options class
[Serializable] 
public class Option
{
    [SerializeField] public string m_sDescription;
    [TextAreaAttribute(2, 20)]
    [SerializeField] public string m_sResultText;
    [TextAreaAttribute(2, 20)]
    [SerializeField] public string m_sSolutionText;
    [SerializeField] public bool m_bEndResult; // If this option leads to death
    [SerializeField] public string m_sTagNext; // Tag name of next situation
    [SerializeField] public OptionSolution m_SkillCheck;

    //[SerializeField] public Char m_SkillToDisplay;
    
    //------------------------------------------------------------------
    public bool CanBeDisplayed(Character character)
    {
        //Debug.Log("--skill maj: " + character.m_iMajor + " min: " + character.m_iMinor);
        //Debug.Log("--skill lvl: " + m_SkillCheck.m_iAbilityLevel + " skil: " + m_SkillCheck.m_iAbility);

        // No skill required
        if(m_SkillCheck.m_iAbilityLevel == OptionSolution.AbilityLevel.NO_SKILL)
        {
            //Debug.Log("no skills");
            return true;
        }

        // Minor skill required
        if(m_SkillCheck.m_iAbilityLevel == OptionSolution.AbilityLevel.MINOR)
        {
            if(character.m_iMinor == m_SkillCheck.m_iAbility || character.m_iMajor == m_SkillCheck.m_iAbility)
            {
                //Debug.Log("minor skills");
                return true;
            }
        }

        // Major skill required 
        if(m_SkillCheck.m_iAbilityLevel == OptionSolution.AbilityLevel.MAJOR)
        {
            if(character.m_iMajor == m_SkillCheck.m_iAbility)
            {
                //Debug.Log("major skills");
                return true;
            }
        }

        //Debug.Log("not available");
        return false;
    }

}

//------------------------------------------------------------------
[Serializable] 
public class OptionSolution
{
    public Ability m_iAbility;
    public AbilityLevel m_iAbilityLevel;

    public enum AbilityLevel
    {
        NO_SKILL = 0,
        MINOR = 1,
        MAJOR = 2,
    } 
}