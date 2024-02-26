using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TextMeshProUGUI mesh;
    public static event Action Redirect;
    private bool isSelectingThis;
    private void Start()
    {
        mesh = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        transform.position = new Vector2(transform.position.x, transform.position.y - StartScreenHandler.incremental * transform.GetSiblingIndex());
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!StartScreenHandler.isSelected)
        {
            isSelectingThis = true;
            StartScreenHandler.mouseValue = transform.GetSiblingIndex();
            mesh.fontStyle = FontStyles.Underline;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!StartScreenHandler.isSelected)
        {
            isSelectingThis = false;
            StartScreenHandler.mouseValue = -1;
            mesh.fontStyle = FontStyles.Normal;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!StartScreenHandler.isSelected && isSelectingThis)
        {
            Redirect?.Invoke();
        }
    }
}
