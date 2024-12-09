using UnityEngine;

[CreateAssetMenu(fileName = "ObjectProgressSkill", menuName = "ScriptableObjects/ObjectProgressSkill")]
public class ObjectProgressSkill : ObjectProgress
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
