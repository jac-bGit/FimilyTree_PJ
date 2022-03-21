using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SituationManagerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject m_GOButton;
    [SerializeField] private float m_fOptionDistance;
    [SerializeField] private Situation m_SituationCurrent;

    // Reference
    public StoryManager m_StoryManager;
    public ScreensManager m_ScreenManager; 
    public Transform m_Illustration;
    public CharacterGen m_charGen;
    private AudioSource m_Audio;

    // UI references 
    [SerializeField] private Text m_StoryTextArea;
    [SerializeField] private RectTransform m_OptionsArea;
    private List<GameObject> m_aButtons = new List<GameObject>();
    [SerializeField] private GameObject m_ButtonNext;
    //[SerializeField] private Image m_Ilustration;

    // Character
    public CharacterBehavior m_CurrentCharacter;

    // Progress variables 
    private bool m_bIsSolved;
    private string m_sTagNextSituation;
    private bool m_bIsDead;
    private string m_sLastSolvedTag = "Intro";

    //------------------------------------------------------------------
    void Awake()
    {
        //m_StoryManager = GameObject.FindObjectOfType<StoryManager>();
        //m_ScreenManager = GameObject.FindObjectOfType<ScreensManager>();
        m_Audio = GetComponent<AudioSource>();
        //m_charGen = GameObject.FindObjectOfType<CharacterGen>();

        //NextSituation("Intro");
    }

    //------------------------------------------------------------------
    void Update()
    {
        
    }

    //------------------------------------------------------------------
    void GenerateOptions()
    {
        m_bIsSolved = (m_SituationCurrent.m_iIsSolvedWith != -1);
        m_bIsDead = false;
        m_sTagNextSituation = "";
        int counting = 0; 

        Debug.Log("generate");

        // Add illustration 
        AddIllustration();

        // Passing solved 
        if(m_bIsSolved)
        {
            Option solution = m_SituationCurrent.m_aOptions[m_SituationCurrent.m_iIsSolvedWith];
            m_sTagNextSituation = solution.m_sTagNext;
            m_ButtonNext.SetActive(true);
            Debug.Log("was solved");
            return;
        }

        //Debug.Log("m_CurrentCharacter.m_Stats: " + m_CurrentCharacter.m_Stats.m_sName);

        int butId = 0;
        // Generation options
        foreach(Option opt in m_SituationCurrent.m_aOptions)
        {
            if(opt.CanBeDisplayed(m_CurrentCharacter.m_Stats))
            {
                Debug.Log("counting: " + m_SituationCurrent.m_aOptions[butId].m_sDescription);
                // Create buttons 
                Vector3 pos = new Vector3(0, -m_fOptionDistance * counting, 0);
                GameObject button = Instantiate(m_GOButton, m_OptionsArea.position, m_OptionsArea.rotation, m_OptionsArea);

                RectTransform rt = button.GetComponent<RectTransform>();  
                rt.localPosition += pos;

                // Set behavior 
                OptionBehavior optionScr = button.GetComponent<OptionBehavior>();
                optionScr.SetOption(m_SituationCurrent.m_aOptions[butId]);
                optionScr.m_OnClicked += OnClickingOptions;

                m_aButtons.Add(button);

                // Loop Progress 
                counting++;
            }

            butId++;
        }

        Debug.Log("butId: " + butId);
    }

    //------------------------------------------------------------------
    void AddIllustration()
    {
        if(m_Illustration == null)
            return;

        // Remove illustration
        if(m_Illustration.childCount > 0)
            Destroy(m_Illustration.GetChild(0).gameObject);

        // Add illustration
        if(m_SituationCurrent.m_GOIllustration != null)
            Instantiate(m_SituationCurrent.m_GOIllustration, m_Illustration.position, m_Illustration.rotation, m_Illustration);
    }

    //------------------------------------------------------------------
    void OnClickingOptions(Option option)
    {
        if(m_bIsSolved)
            return;

        // Add response 
        m_StoryTextArea.text += "\n \n" + option.m_sResultText;

        if(!option.m_bEndResult)
        {
            // Continue
            //Debug.Log("option.m_sTagNext: " + option.m_sTagNext);
            m_sLastSolvedTag = m_SituationCurrent.m_sTag;
            m_sTagNextSituation = option.m_sTagNext;

            for(int i = 0; i < m_SituationCurrent.m_aOptions.Length; i++)
            {
                if(m_SituationCurrent.m_aOptions[i] == option)
                    m_SituationCurrent.m_iIsSolvedWith = i;
            }
        }
        else
        {
            // Death
            m_sTagNextSituation = "Intro";
            m_bIsDead = true;
        }

        m_ButtonNext.SetActive(true);
        m_bIsSolved = true;
    }

    //------------------------------------------------------------------
    public void NextSituation(string tag)
    {
        Debug.Log("story manager: " + m_StoryManager);
        Debug.Log("story res: " + m_StoryManager.m_aSituations.Count);
        m_SituationCurrent = m_StoryManager.GetSituationByTag(tag);

        // Remove buttons 
        foreach(GameObject button in m_aButtons)
        {
            // Remove listening 
            OptionBehavior optionScr = button.GetComponent<OptionBehavior>();
            optionScr.m_OnClicked -= OnClickingOptions;
            // Destroy
            Destroy(button);
        }

        m_aButtons.Clear();

        //m_StoryTextArea.text = m_SituationCurrent.m_sDesription;
        // setting text 
        if(m_SituationCurrent.m_iIsSolvedWith == -1)
            m_StoryTextArea.text = m_SituationCurrent.m_sDesription;
        else
        {
            Option solution = m_SituationCurrent.m_aOptions[m_SituationCurrent.m_iIsSolvedWith];
            m_StoryTextArea.text = solution.m_sSolutionText;
        }

        m_ButtonNext.SetActive(false);
        GenerateOptions();

        // Show end credits 
        if(m_SituationCurrent.m_sTag == "Ending")
            m_ScreenManager.ActiveScreen("End", true);
    }

    //------------------------------------------------------------------
    public void ClickNext()
    {

        // Death 
        if(m_bIsDead)
        {
            m_ScreenManager.SetActiveScreen("Ancestors");
            //m_charGen.GenerateFather(m_CurrentCharacter);
            //m_charGen.DoGen();
            DefaultValues();
            return;
        }

        // Continue
        //Debug.Log("m_sTagNextSituation: " + m_sTagNextSituation);
        NextSituation(m_sTagNextSituation);

        m_Audio.Play();
    }

    //------------------------------------------------------------------
    public void DefaultValues()
    {
        NextSituation("Intro");
    }

    //------------------------------------------------------------------
    public void ToLastSolvedSituation()
    {
        NextSituation(m_sLastSolvedTag);
    }
}
