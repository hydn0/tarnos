using System;
using System.Linq;
using System.Reflection;
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
                float normalizedLevel = progressSkill.Level / objectProgressSkill.EffectScaling.MaxLevel;
                float curveY = objectProgressSkill.EffectScaling.Curve.Evaluate(normalizedLevel);

                var matchingVector = objectProgressSkill.EffectScaling.curveYAndMultiplier
                    .FirstOrDefault(v => Mathf.Approximately(v.x, curveY));

                if (matchingVector != default(Vector2))
                {
                    progressSkill.Modifier.Multiplier = matchingVector.y;
                }
            }
        }
        ApplyModifierToGlobal(progressSkill);
    }

    private void ApplyModifierToGlobal(ProgressSkill progressSkill)
    {
        Type globalModifiersClass = typeof(ObjectGlobalModifiers);
        foreach (FieldInfo fieldInfo in globalModifiersClass.GetFields())
        {
            if (fieldInfo.Name == progressSkill.Modifier.Name.ToString())
            {
                fieldInfo.SetValue(objectGlobalModifiers, progressSkill.Modifier.Multiplier);
            }
        }
    }
}
