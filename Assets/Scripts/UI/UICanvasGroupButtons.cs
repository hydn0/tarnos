using UnityEngine;

public class UICanvasGroupButtons : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup[] canvasGroups;

    public void SetVisible(CanvasGroup newCanvasGroup)
    {
        foreach (CanvasGroup canvasGroup in canvasGroups)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        newCanvasGroup.alpha = 1;
        newCanvasGroup.interactable = true;
        newCanvasGroup.blocksRaycasts = true;
    }
}