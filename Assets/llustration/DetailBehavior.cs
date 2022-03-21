using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DetailBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void HoverDetialsInvoker(bool isHover);
    public event HoverDetialsInvoker m_OnHover;

    [TextAreaAttribute(2, 10)]
    public string m_sHintText;
    private Text m_text;

    private CustomCursor m_CustomCursor;

    void Start()
    {
        //m_CustomCursor = GameObject.FindObjectOfType<CustomCursor>();

        m_text = GetComponentInChildren<Text>();
        m_text.text = m_sHintText;
        m_text.gameObject.SetActive(false);
    }


    //------------------------------------------------------------------
    private void OnHoverInvoke(bool isHover)
    {
        if(m_OnHover != null)
            m_OnHover(isHover);
    }

    //------------------------------------------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverInvoke(true);
        m_text.gameObject.SetActive(true);
        //m_CustomCursor.SetCursor(true);
    }

    //------------------------------------------------------------------
    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverInvoke(false);
        m_text.gameObject.SetActive(false);
        //m_CustomCursor.SetCursor(false);
    }
}

//------------------------------------------------------------------
/*[Serializable]
public class Detail
{
    [TextAreaAttribute(2, 10)]
    public string m_sHintText;
}*/
