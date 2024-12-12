using UnityEngine;

[CreateAssetMenu(fileName = "ObjectGlobalModifiers", menuName = "ScriptableObjects/ObjectGlobalModifiers")]
public class ObjectGlobalModifiers : ScriptableObject
{
    public float AllExperience;
    public float JobExperience;
    public float SkillExperience;
    public float JobIncome;

    public enum ModifierNames
    {
        AllExperience,
        JobExperience,
        SkillExperience,
        JobIncome
    }

    [System.Serializable]
    public struct Modifier
    {
        public ModifierNames Name;
        public float Multiplier;
    }
}
