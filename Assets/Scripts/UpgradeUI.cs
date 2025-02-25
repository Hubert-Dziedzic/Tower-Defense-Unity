using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        UIManage.main.SetHoverState(true);
        
    }    
    
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        UIManage.main.SetHoverState(false);
        gameObject.SetActive(false);
    }
}
