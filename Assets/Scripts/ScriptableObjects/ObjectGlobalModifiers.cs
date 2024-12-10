using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectGlobalModifiers", menuName = "ScriptableObjects/ObjectGlobalModifiers")]
public class ObjectGlobalModifiers : ScriptableObject
{
    public List<Modifier> GlobalModifiers;

    public enum ModifierID
    {
        AllExperience,
        JobExperience,
        JobIncome,
        SkillExperience
    }

    [System.Serializable]
    public struct Modifier
    {
        public ModifierID ID;
        public float Multiplier;
    }
}
