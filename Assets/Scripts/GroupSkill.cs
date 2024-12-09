using UnityEngine;

public class GroupSkill : Group
{
    [SerializeField] private ObjectProgressSkill[] skills;

    protected override ObjectProgress[] Objects => skills;

    protected override void InitializeProgressObject(Progress newProgress, ObjectProgress baseObj)
    {
        ProgressSkill progressSkill = (ProgressSkill)newProgress;
        ObjectProgressSkill skill = (ObjectProgressSkill)baseObj;
        progressSkill.InitializeSkill(skill.name, "Skill", skill.Effect, skill.DailyExperience * experienceModifier);
    }
}

