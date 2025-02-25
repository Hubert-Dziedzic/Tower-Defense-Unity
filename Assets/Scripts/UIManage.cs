using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using TMPro.EditorUtilities;
#endif
using UnityEngine;

public class UIManage : MonoBehaviour
{
    public static UIManage main;

    private bool isHoverUI;

    private void Awake()
    {
        main = this;
    }

    public void SetHoverState(bool state)
    {
        isHoverUI = state;
    }

    public bool IsHoverUI()
    {
        return isHoverUI;
    }
}
