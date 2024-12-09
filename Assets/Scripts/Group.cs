using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public abstract class Group : MonoBehaviour
{
    [SerializeField] protected float experienceModifier = 1f;
    [SerializeField] protected Progress progress;
    protected List<Progress> progressList = new();
    protected int progressCount = 1;

    [Header("UI")]
    [SerializeField] protected GameObject panel;
    [SerializeField] protected TextMeshProUGUI nextRequirementsText;

    protected abstract BaseObject[] Objects { get; }

    private void Awake()
    {
        InstantiateObjects();
        if (progressCount < Objects.Length)
        {
            DisplayNextRequirements(Objects[progressCount].Requirements);
        }
    }

    protected void InstantiateObjects()
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            Progress newObject = Instantiate(progress, panel.transform);
            InitializeProgressObject(newObject, Objects[i]);
            newObject.LeveledUp.AddListener(OnLeveledUp);
            progressList.Add(newObject);
            if (i != 0)
            {
                newObject.gameObject.SetActive(false);
            }
        }
    }

    protected abstract void InitializeProgressObject(Progress newProgress, BaseObject baseObj);

    protected void OnLeveledUp()
    {
        if (progressCount < Objects.Length && AreAllRequirementsMet(Objects[progressCount].Requirements))
        {
            ActivateObjects(Objects[progressCount].name);
            progressCount += 1;

            if (progressCount < Objects.Length)
            {
                DisplayNextRequirements(Objects[progressCount].Requirements);
            }
            else
            {
                nextRequirementsText.text = "";
            }
        }
    }

    private bool AreAllRequirementsMet(IEnumerable<BaseObject.Requirement> requirements)
    {
        return requirements.All(r => 
            progressList.Any(o => o.name == r.Object.name && o.Level >= r.Level));
    }

    private void DisplayNextRequirements(List<BaseObject.Requirement> requirements)
    {
        string nextRequirements = "Required:";
        foreach (var requirement in requirements)
        {
            nextRequirements += " " + requirement.Object.name + ": " + requirement.Level;
        }
        nextRequirementsText.text = nextRequirements;
    }

    private void ActivateObjects(string objectName)
    {
        foreach (var obj in progressList.Where(o => o.name == objectName))
        {
            obj.gameObject.SetActive(true);
        }
    }
}
