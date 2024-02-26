using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorDefault;
    [SerializeField]
    private Texture2D cursorAim;
    private Vector2 cursorHotspot;
    // Start is called before the first frame update
    void Start()
    {
        ChangeCursor(cursorDefault);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ChangeCursor(cursorAim);
        }
        if(Input.GetMouseButtonUp(0)) 
        { 
            ChangeCursor(cursorDefault);
        }
    }
    private void ChangeCursor(Texture2D cursorTexture)
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }
}
