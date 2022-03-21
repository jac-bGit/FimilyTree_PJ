using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IllustrationBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public DetailBehavior[] m_aDetails;
    private CustomCursor m_CustomCursor;

    // Start is called before the first frame update
    void Start()
    {
        m_aDetails = GetComponentsInChildren<DetailBehavior>();
        m_CustomCursor = GameObject.FindObjectOfType<CustomCursor>();
    }

    //------------------------------------------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_CustomCursor.SetCursor(true);
    }

    //------------------------------------------------------------------
    public void OnPointerExit(PointerEventData eventData)
    {
        m_CustomCursor.SetCursor(false);
    }
}
