using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public abstract class Group : MonoBehaviour
{
    [SerializeField] protected float experienceModifier = 1f;
    [SerializeField] protected Progress progressObject;
    protected List<Progress> progressObjects = new();
    protected int progressObjectCount = 1;

    [Header("UI")]
    [SerializeField] protected GameObject panel;
    [SerializeField] protected TextMeshProUGUI nextRequirementsText;

    protected abstract BaseObject[] Objects { get; }

    void Awake()
    {
        InstantiateObjects();
        if (progressObjectCount < Objects.Length)
        {
            DisplayNextRequirements(Objects[progressObjectCount].Requirements);
        }
    }

    protected void InstantiateObjects()
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            Progress newObject = Instantiate(progressObject, panel.transform);
            InitializeProgressObject(newObject, Objects[i]);
            newObject.LeveledUp.AddListener(OnLeveledUp);
            progressObjects.Add(newObject);
            if (i != 0)
            {
                newObject.gameObject.SetActive(false);
            }
        }
    }

    protected abstract void InitializeProgressObject(Progress newProgressObject, BaseObject baseObj);

    protected void OnLeveledUp()
    {
        if (progressObjectCount < Objects.Length && AreAllRequirementsMet(Objects[progressObjectCount].Requirements))
        {
            ActivateObjects(Objects[progressObjectCount].name);
            progressObjectCount += 1;

            if (progressObjectCount < Objects.Length)
            {
                DisplayNextRequirements(Objects[progressObjectCount].Requirements);
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
            progressObjects.Any(o => o.name == r.Object.name && o.Level >= r.Level));
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
        foreach (var obj in progressObjects.Where(o => o.name == objectName))
        {
            obj.gameObject.SetActive(true);
        }
    }
}
