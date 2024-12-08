using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Group : MonoBehaviour
{
    [SerializeField] protected float experienceModifier = 1f;
    [SerializeField] protected Progress progressObject;
    protected List<Progress> progressObjects = new();
    protected int progressObjectCount = 1;
    [Header("UI")]
    [SerializeField] protected GameObject panel;
    [SerializeField] protected TextMeshProUGUI nextRequirementsText;
}
