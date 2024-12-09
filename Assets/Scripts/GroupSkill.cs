using UnityEngine;

public class GroupSkill : Group
{
    [SerializeField] private BaseSkill[] skills;

    protected override BaseObject[] Objects => skills;

    protected override void InitializeProgressObject(Progress newProgress, BaseObject baseObj)
    {
        ProgressSkill progressSkill = (ProgressSkill)newProgress;
        BaseSkill skill = (BaseSkill)baseObj;
        progressSkill.InitializeSkill(skill.name, "Skill", skill.Effect, skill.DailyExperience * experienceModifier);
    }
}

