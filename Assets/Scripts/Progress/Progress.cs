using UnityEngine;

/// <summary>
/// Manages exp, levels, and requirements.
/// </summary>
public class Progress : MonoBehaviour
{
    [SerializeField]
    private RequirementProgress[] _requirements;
    [SerializeField]
    private string _requirementID;
    [SerializeField]
    private float _dailyExp = 1;
    [SerializeField]
    private float _maxExp = 100;
    private float _currentExp;

    public float CurrentExp
    {
        get => _currentExp;
        protected set
        {
            if (value >= _maxExp)
            {
                _currentExp = 0;
                _maxExp += 100;
                Level += 1;
            }
            else
            {
                _currentExp = value;
            }
        }
    }

    public int Level { get; private set; }
    public float DailyExp => _dailyExp;
    public float MaxExp => _maxExp;
    public RequirementProgress[] Requirements => _requirements;
    public string RequirementID => _requirementID;
}
