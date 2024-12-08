using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupSkill : Group
{
    [SerializeField] private BaseSkill[] skills;

    void Awake()
    {
        InstantiateSkills();
        DisplayNextRequirements(skills[progressObjectCount].Requirements);
    }

    public void OnSkillLeveledUp()
    {
        if (progressObjectCount < skills.Length && AreAllRequirementsMet(skills[progressObjectCount].Requirements))
        {
            ActivateSkillObjects(skills[progressObjectCount].name);
            progressObjectCount += 1;
            if (progressObjectCount < skills.Length)
            {
                DisplayNextRequirements(skills[progressObjectCount].Requirements);
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
            progressObjects.Any(o => o.name == r.Object.name && o.Level == r.Level));
    }

    private void ActivateSkillObjects(string skillName)
    {
        foreach (var obj in progressObjects.Where(o => o.name == skillName))
        {
            obj.gameObject.SetActive(true);
        }
    }

    private void DisplayNextRequirements(List<BaseObject.Requirement> requirements)
    {
        string nextRequirements = "Required:";
        foreach (BaseObject.Requirement requirement in requirements)
        {
            nextRequirements += " " + requirement.Object.name + ": " + requirement.Level;
        }
        nextRequirementsText.text = nextRequirements;
    }

    private void InstantiateSkills()
    {
        List<Progress> skillObjects = new();

        for (int i = 0; i < skills.Length; i++)
        {
            Progress newSkillObject = Instantiate(progressObject, panel.transform);
    
            BaseSkill skill = skills[i];
            newSkillObject.InitializeSkill(skill.name, "Skill", skill.Effect, skill.DailyExperience * experienceModifier);

            newSkillObject.LeveledUp.AddListener(OnSkillLeveledUp);
            progressObjects.Add(newSkillObject);
            if (i != 0)
            {
                newSkillObject.gameObject.SetActive(false);
            }
        }
    }
}
