using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public Dictionary<ID, float> Effects { get; private set; } = new();

    public enum ID
    {
        GameSpeed,
        AllExperience,
        JobExperience,
        SkillExperience,
        JobIncome
    }

    public void ResetEffects()
    {
        Effects.Clear();
        foreach (ID id in System.Enum.GetValues(typeof(ID)))
        {
            Effects.Add(id, 1);
        }
    }
}