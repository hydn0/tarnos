using UnityEngine;

public class CanvasGroupManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] canvasGroups;

    public void OnCanvasButtonClick(CanvasGroup newCanvasGroup)
    {
        foreach (CanvasGroup canvasGroup in canvasGroups)
        {
            if (newCanvasGroup == canvasGroup)
            {
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }
        }
    }
}
