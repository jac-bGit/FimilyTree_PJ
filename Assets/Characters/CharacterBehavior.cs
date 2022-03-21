using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterBehavior : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{ 
    public string m_sName; 
    public Character m_Stats;

    // Face part refs
    public Image m_face;
    public Image m_hair;
    public Image m_beard;
    public Image m_eyes;
    public Image m_mouth;
    public Color m_cHairColor;

    // Texts refs 
    public Text m_txtName;
    public Text m_txtClass;
    public Text m_txtSkillMajor;
    public Text m_txtSkillMinor;
    public Image m_Ornament;
    public Color m_cDefault;
    public Color m_cHover;

    private bool m_bIsSelected = false;

    public delegate void ClickOption(CharacterBehavior character);
    public event ClickOption m_OnClicked;

    //------------------------------------------------------------------
    private void OnClickedInvoke(CharacterBehavior character)
    {
        if(m_OnClicked != null)
            m_OnClicked(character);
    }

    //------------------------------------------------------------------
    private void Awake() 
    {
        if(m_Ornament != null)
            m_Ornament.color = m_cDefault;
    }

    //------------------------------------------------------------------
    public void SetupTextStat(string name, Character charStat)
    {
        m_sName = name;
        m_Stats = charStat;

        m_txtName.text = m_sName;
        m_txtClass.text = m_Stats.m_sName;
        m_txtSkillMajor.text = "Major: " + MajorSkillToText(m_Stats);
        m_txtSkillMinor.text = "Minor: " + MinorSkillToText(m_Stats);
    }

    //------------------------------------------------------------------
    public void OnPointerClick(PointerEventData eventData)
    {
        // Select character
        OnClickedInvoke(this);
    }

    //------------------------------------------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(m_bIsSelected)
            return; 

        if(m_Ornament != null)
            m_Ornament.color = m_cHover;
    }

    //------------------------------------------------------------------
    public void OnPointerExit(PointerEventData eventData)
    {
        if(m_bIsSelected)
            return; 

        if(m_Ornament != null)
            m_Ornament.color = m_cDefault;
    }

    //------------------------------------------------------------------
    public void SetSelectionColor(bool isSelected)
    {
        m_bIsSelected = isSelected;

        if(m_bIsSelected)
            m_Ornament.color = m_cHover;
        else
            m_Ornament.color = m_cDefault;
    }

    //------------------------------------------------------------------
    public void SetupFace(Sprite face, Sprite hair, Sprite beard, Sprite eyes, Sprite mouth, Color col)
    {
        if(hair != null)
            m_hair.gameObject.SetActive(true);

        if(beard != null)
            m_beard.gameObject.SetActive(true);

        m_cHairColor = col;
        m_face.sprite = face;
        m_hair.sprite = hair;
        m_hair.color = col;
        m_beard.sprite = beard;
        m_beard.color = col;
        m_eyes.sprite = eyes;
        m_mouth.sprite = mouth;

        if(hair == null)
            m_hair.gameObject.SetActive(false);

        if(beard == null)
            m_beard.gameObject.SetActive(false);
    }

    //------------------------------------------------------------------
    string MajorSkillToText(Character charStat)
    {
        string txt = "";

        switch(charStat.m_iMajor)
        {
            case Ability.STRENGHT:
                txt = "Very strong";
            break;
            case Ability.DEXTERITY:
                txt = "Highly dexterous";
            break;
            case Ability.INTELINGENCE:
                txt = "Genius";
            break;
            case Ability.SPEED:
                txt = "Very fast";
            break;
            case Ability.CHARISMA:
                txt = "Great speaker";
            break;
        }

        return txt;
    }

    //------------------------------------------------------------------
    string MinorSkillToText(Character charStat)
    {
        string txt = "";

        switch(charStat.m_iMinor)
        {
            case Ability.STRENGHT:
                txt = "Quite strong";
            break;
            case Ability.DEXTERITY:
                txt = "Dexterous";
            break;
            case Ability.INTELINGENCE:
                txt = "Clever";
            break;
            case Ability.SPEED:
                txt = "Quick";
            break;
            case Ability.CHARISMA:
                txt = "Talkative";
            break;
        }

        return txt;
    }
}
