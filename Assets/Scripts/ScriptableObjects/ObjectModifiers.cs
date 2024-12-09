using UnityEngine;

[CreateAssetMenu(fileName = "ObjectModifiers", menuName = "ScriptableObjects/ObjectModifiers")]
public class ObjectModifiers : ScriptableObject
{
    public enum ModifierID
    {
        AllExperience,
        JobExperience,
        JobIncome,
        SkillExperience
    }

    public Modifier[] Modifiers;

    [System.Serializable]
    public struct Modifier
    {
        public ModifierID ID;
        public float Multiplier;
    }
}
