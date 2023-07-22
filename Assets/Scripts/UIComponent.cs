using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour
{
    public CanvasGroup uiComponent_CanvasGroup;
    public bool isUiComponentActive = false;

    void Awake()
    {
        if (uiComponent_CanvasGroup == null)
            uiComponent_CanvasGroup = GetComponent<CanvasGroup>();
        Init();
    }

    void Init(bool isActive = false)
    {
        if (!isActive)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    public void TurnOff()
    {
        uiComponent_CanvasGroup.CanvasGroupFade(0);
        uiComponent_CanvasGroup.CanvasGroupInteractable(false);
    }

    public void TurnOn()
    {
        uiComponent_CanvasGroup.CanvasGroupFade(1);
        uiComponent_CanvasGroup.CanvasGroupInteractable(true);
    }
}
