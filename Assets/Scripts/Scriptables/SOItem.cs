using UnityEngine;

public class SOItem : ScriptableObject
{
    public Requirement[] Requirements;
    public Effect.ID Effect;
    public float Multiplier;
    public float DailyExpense;
}
