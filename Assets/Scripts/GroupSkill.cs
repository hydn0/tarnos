using UnityEngine;

public class GroupSkill : Group
{
    [SerializeField] private BaseSkill[] skills;

    protected override BaseObject[] Objects => skills;

    protected override void InitializeProgressObject(Progress newProgressObject, BaseObject baseObj)
    {
        BaseSkill skill = (BaseSkill)baseObj;
        newProgressObject.InitializeSkill(skill.name, "Skill", skill.Effect, skill.DailyExperience * experienceModifier);
    }
}

