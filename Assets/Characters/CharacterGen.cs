using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGen : MonoBehaviour
{
    public CharacterBehavior[] m_aCharacterBehaviors;
    public CharacterBehavior m_Father;

    // Names 
    public string[] m_aNames;

    // Char classes 
    public Character[] m_aCharStats; 

    // Face features 
    public Sprite[] m_aFaces;
    public Sprite[] m_aHair;
    public Sprite[] m_aBeards;
    public Sprite[] m_aEyes;
    public Sprite[] m_aMouths;
    public Color[] m_aColors;

    // Selection
    private int m_iSelected = -1;
    private ScreensManager m_ScreenManager; 
    public SituationManagerBehavior m_SituationManager;
    public Text m_TextName;
    public Text m_TextClass;
    public Text m_TextMajor;
    public Text m_TextMinor;
    public Button m_BtnAvenge;

    //------------------------------------------------------------------
    private void OnEnable() 
    {
        m_ScreenManager = GameObject.FindObjectOfType<ScreensManager>();
        //m_SituationManager = GameObject.FindObjectOfType<SituationManagerBehavior>();
        GenerateCharacters();
        Debug.Log("gen faces");

        m_TextName.gameObject.SetActive(false);
        m_TextClass.gameObject.SetActive(false);
        m_TextMajor.gameObject.SetActive(false);
        m_TextMinor.gameObject.SetActive(false);
        m_BtnAvenge.gameObject.SetActive(false);
    }

    //------------------------------------------------------------------
    public void DoGen() 
    {
        m_ScreenManager = GameObject.FindObjectOfType<ScreensManager>();
        //m_SituationManager = GameObject.FindObjectOfType<SituationManagerBehavior>();
        GenerateCharacters();
        Debug.Log("gen faces");

        m_TextName.gameObject.SetActive(false);
        m_TextClass.gameObject.SetActive(false);
        m_TextMajor.gameObject.SetActive(false);
        m_TextMinor.gameObject.SetActive(false);
         m_BtnAvenge.gameObject.SetActive(false);
    }

    //------------------------------------------------------------------
    void GenerateCharacters()
    {
        foreach(CharacterBehavior character in m_aCharacterBehaviors)
        {
            // choose class
            int randStats = Random.Range(0, m_aCharStats.Length); 
            character.m_Stats = m_aCharStats[randStats];
            int randName = Random.Range(0, m_aNames.Length); 

            character.SetupTextStat(m_aNames[randName], character.m_Stats);

            
            // set face
            int randFace = Random.Range(0, m_aFaces.Length);
            // set hair
            //int randHair = Random.Range(-1, m_aHair.Length);
            // set beard
            //int randBerd = Random.Range(-1, m_aBeards.Length);
            // set eyes
            int randEyes = Random.Range(0, m_aEyes.Length);
            // set mouth
            int randMouth = Random.Range(0, m_aMouths.Length);

            int randColor = Random.Range(0, m_aColors.Length);

            character.SetupFace(m_aFaces[randFace], SetHair(m_aHair), SetHair(m_aBeards), m_aEyes[randEyes], m_aMouths[randMouth], m_aColors[randColor]);
            

            character.m_OnClicked += SelectCharacter;
        }
    }

    //------------------------------------------------------------------
    public void GenerateFather(CharacterBehavior father)
    {
        if(father == null)
            return;

        //m_Father = father;

        m_Father.SetupFace(father.m_face.sprite, father.m_hair.sprite, father.m_beard.sprite, father.m_eyes.sprite, father.m_mouth.sprite, father.m_cHairColor);
    }
    

    //------------------------------------------------------------------
    Sprite SetHair(Sprite[] array)
    {
        int rand = Random.Range(-1, array.Length);
        if(rand > -1)
            return array[rand];

        return null;
    }

    //------------------------------------------------------------------
    void SelectCharacter(CharacterBehavior character)
    {
        int id = 0;

        foreach(CharacterBehavior ch in m_aCharacterBehaviors)
        {
            if(ch == character)
            {
                ch.SetSelectionColor(true);
                m_iSelected = id;
                continue;
            }

            ch.SetSelectionColor(false);
            id++;
        }

        // Set Texts 
        m_TextName.gameObject.SetActive(true);
        //m_TextClass.gameObject.SetActive(true);
        m_TextMajor.gameObject.SetActive(true);
        m_TextMinor.gameObject.SetActive(true);
        m_BtnAvenge.gameObject.SetActive(true);

        m_TextName.text = character.m_sName + " - " + character.m_Stats.m_sName;
        //m_TextClass.text = character.m_Stats.m_sName;
        m_TextMajor.text = MajorSkillToText(character.m_Stats);
        m_TextMinor.text = MinorSkillToText(character.m_Stats);
    }

    //------------------------------------------------------------------
    public void ConfirmCharacter()
    {
        if(m_iSelected == -1)
            return;

        Debug.Log("m_iSelected: " + m_iSelected);
        m_SituationManager.m_CurrentCharacter = m_aCharacterBehaviors[m_iSelected];
        GenerateFather(m_SituationManager.m_CurrentCharacter);
        // Start adventure
        m_ScreenManager.SetActiveScreen("Story");
        m_SituationManager.ToLastSolvedSituation();

        // Reset 
        m_iSelected = -1;
        SelectCharacter(null); 
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
