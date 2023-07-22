using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIExtend
{

    public static IEnumerator UI_Start(this UIComponent uiComponent)
    {
        uiComponent.isUiComponentActive = true;
        bool isUiComponentActive = uiComponent.isUiComponentActive;

        uiComponent.uiComponent_CanvasGroup.CanvasGroupInteractable(isUiComponentActive);
        float maxTime = 0.15f;
        for (float i = 0; i < maxTime; i += Time.deltaTime)
        {
            uiComponent.uiComponent_CanvasGroup.CanvasGroupFade(i / maxTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        uiComponent.uiComponent_CanvasGroup.CanvasGroupFade(1);

        yield return null;
    }

    public static IEnumerator UI_End(this UIComponent uiComponent)
    {
        uiComponent.isUiComponentActive = false;
        bool isUiComponentActive = uiComponent.isUiComponentActive;

        uiComponent.uiComponent_CanvasGroup.CanvasGroupInteractable(isUiComponentActive);
        float maxTime = 0.15f;
        for (float i = maxTime; i > 0; i -= Time.deltaTime)
        {
            uiComponent.uiComponent_CanvasGroup.CanvasGroupFade(i / maxTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        uiComponent.uiComponent_CanvasGroup.CanvasGroupFade(0);
        yield return null;
    }
}
