using UnityEngine;

public class GroupSkill : Group
{
    [SerializeField] private ObjectGlobalModifiers objectGlobalModifiers;
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
        ProgressSkill progressSkill = (ProgressSkill)progress;
        foreach (ObjectProgressSkill objectProgressSkill in objectSkills)
        {
            if (progressSkill.name == objectProgressSkill.name)
            {
                progressSkill.Effect.Multiplier = objectProgressSkill.EffectScaling.Evaluate(progressSkill.Level);
            }
        }

        for (int i = 0; i < objectGlobalModifiers.GlobalModifiers.Count; i++)
        {
            if (objectGlobalModifiers.GlobalModifiers[i].ID == progressSkill.Effect.ID)
            {
                ObjectGlobalModifiers.Modifier modifier = objectGlobalModifiers.GlobalModifiers[i];
                modifier.Multiplier = progressSkill.Effect.Multiplier;
                objectGlobalModifiers.GlobalModifiers[i] = modifier;
            }
        }
    }
}
