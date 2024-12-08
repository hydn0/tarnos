using UnityEngine;

[CreateAssetMenu(fileName = "BaseSkill", menuName = "ScriptableObjects/BaseSkill")]
public class BaseSkill : BaseObject
{
    public enum Modifiers
    {
        AllExperience,
        JobExperience,
        JobIncome,
        SkillExperience
    }

    public Modifier Effect;

    [System.Serializable]
    public struct Modifier
    {
        public Modifiers Name;
        public float Multiplier;
    }
}
