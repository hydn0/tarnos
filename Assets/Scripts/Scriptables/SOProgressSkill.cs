using UnityEngine;

[CreateAssetMenu(fileName = "SOProgressSkill", menuName = "ScriptableObjects/SOProgressSkill")]
public class SOProgressSkill : SOProgress
{
    public Effect.ID Effect;
    public float Multiplier;
}
