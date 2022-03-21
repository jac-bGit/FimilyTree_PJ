using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D m_TextureDefault;
    public Texture2D m_TextureDetail;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotDefault = Vector2.zero;
    public Vector2 hotSpotDetail = Vector2.zero;

    //------------------------------------------------------------------
    void Start()
    {
        Cursor.SetCursor(m_TextureDefault, hotSpotDefault, cursorMode);
    }

    //------------------------------------------------------------------
    public void SetCursor(bool isDetail)
    {
        if(isDetail)
            Cursor.SetCursor(m_TextureDetail, hotSpotDetail, cursorMode);
        else
            Cursor.SetCursor(m_TextureDefault, hotSpotDefault, cursorMode);
    }
}
