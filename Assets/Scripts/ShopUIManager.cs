using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    bool hasCollided = false;
    [SerializeField] CanvasGroup panelCanvasGroup;
    [SerializeField] GameObject player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasCollided)
        {
            ShopManager._instance.ResetSection();
            player = other.gameObject;
            panelCanvasGroup.CanvasGroupInteractable(true);
            panelCanvasGroup.CanvasGroupFade(1);

            hasCollided = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && hasCollided)
        {
            Exit();
        }
    }

    public void Exit()
    {
        player.GetComponent<PlayerAnimationController>().SetClothesFromAssets();
        panelCanvasGroup.CanvasGroupInteractable(false);
        panelCanvasGroup.CanvasGroupFade(0);

        hasCollided = false;
    }

}
