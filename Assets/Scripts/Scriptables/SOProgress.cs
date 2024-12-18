using UnityEngine;

public class SOProgress : ScriptableObject
{
    public Requirement[] Requirements;
    public float DailyExp = 1;

    private float _currentExp;

    public float CurrentExp
    {
        get => _currentExp;
        set
        {
            if (value >= MaxExp)
            {
                _currentExp = 0;
                MaxExp += 10;
                Level += 1;
            }
            else
            {
                _currentExp = value;
            }
        }
    }

    public float MaxExp { get; private set; } = 10;
    public int Level { get; private set; }

    public virtual void ResetEXP()
    {
        _currentExp = 0;
        MaxExp = 10;
        Level = 0;
    }
}
