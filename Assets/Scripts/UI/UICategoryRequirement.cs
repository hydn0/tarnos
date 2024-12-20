using UnityEngine;
using UnityEngine.UI;

public class UICategoryRequirement : MonoBehaviour
{
    [SerializeField]
    private Text _nextRequirements;
    private CategoryRequirement _categoryRequirement;

    private void Awake()
    {
        _categoryRequirement = GetComponent<CategoryRequirement>();
    }

    private void LateUpdate()
    {
        _nextRequirements.text = FormatToText(_categoryRequirement.GetNextRequirements());
    }

    private string FormatToText(RequirementProgress[] requirements)
    {
        string nextRequirementText = "Required:";
        
        if (requirements != null)
        {
            foreach (RequirementProgress requirement in requirements)
            {
                nextRequirementText += $" {requirement.ProgressID}: {requirement.Level}";
            }
        }
        else
        {
            nextRequirementText = "";
        }

        return nextRequirementText;
    }
}