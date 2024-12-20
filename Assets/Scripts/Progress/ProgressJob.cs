using UnityEngine;

public class ProgressJob : Progress
{
    [SerializeField]
    private float _dailyIncome = 1;

    public static ProgressJob CurrentJob { get; set; }
    public float DailyIncome => _dailyIncome;

    private void Update()
    {
        if (CurrentJob == this)
        {
            CurrentExp += DailyExp;
        }
    }
}