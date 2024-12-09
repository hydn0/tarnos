using UnityEngine;

public class GroupSkill : Group
{
    [SerializeField] private ObjectModifiers objectModifiers;
    [SerializeField] private ObjectProgressSkill[] objectSkills;

    protected override ObjectProgress[] Objects => objectSkills;

    protected override void InitializeProgressObject(Progress newProgress, ObjectProgress progressObj)
    {
        ProgressSkill progressSkill = (ProgressSkill)newProgress;
        ObjectProgressSkill skill = (ObjectProgressSkill)progressObj;
        progressSkill.InitializeSkill(skill.name, "Skill", skill.Effect, skill.DailyExperience * experienceModifier);
    }

    protected override void ScaleProgress(Progress progress)
    {
        return;
    }
}
