using UnityEngine;

[CreateAssetMenu(fileName = "ObjectProgressJob", menuName = "ScriptableObjects/ObjectProgressJob")]
public class ObjectProgressJob : ObjectProgress
{
    public float DailyIncome = 1f;
    public AnimationCurve IncomeScaling;
}