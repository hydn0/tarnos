using UnityEngine;
using System.Linq;

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
                float normalizedLevel = progressSkill.Level / objectProgressSkill.EffectScaling.MaxLevel;
                float curveY = objectProgressSkill.EffectScaling.Curve.Evaluate(normalizedLevel);

                var matchingVector = objectProgressSkill.EffectScaling.curveYAndMultiplier
                    .FirstOrDefault(v => Mathf.Approximately(v.x, curveY));

                if (matchingVector != default(Vector2))
                {
                    progressSkill.Effect.Multiplier = matchingVector.y;
                }
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
