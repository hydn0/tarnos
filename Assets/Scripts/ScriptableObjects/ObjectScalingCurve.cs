using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectScalingCurve", menuName = "ScriptableObjects/ObjectScalingCurve")]
public class ObjectScalingCurve : ScriptableObject
{
    public AnimationCurve Curve;
    public int MaxLevel;
    public List<Vector2> curveYAndMultiplier;
}
