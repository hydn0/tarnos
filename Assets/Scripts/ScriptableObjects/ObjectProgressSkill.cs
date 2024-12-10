using UnityEngine;

[CreateAssetMenu(fileName = "ObjectProgressSkill", menuName = "ScriptableObjects/ObjectProgressSkill")]
public class ObjectProgressSkill : ObjectProgress
{
    public ObjectGlobalModifiers.Modifier Effect;
    public AnimationCurve EffectScaling;
}
