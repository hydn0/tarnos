using System.Collections;
using UnityEngine;

public class ProgressJob : Progress
{
    public float DailyIncome;
    private float _startIncome;

    protected override void Start()
    {
        base.Start();
        _incomeOrEffectMuteText.text = "Income";
        _startIncome = DailyIncome;
        Selected.AddListener(_player.NewJobActivated);
    }

    public void InitializeJob(string progressName, string progressTag, float dailyIncome, float dailyExperience)
    {
        name = progressName;
        tag = progressTag;
        DailyIncome = dailyIncome;
        DailyExperience = dailyExperience;
        UpdateUI();
    }

    public override IEnumerator IncrementXP()
    {
        while (true)
        {
            float experienceAddend = DailyExperience * _objectGlobalModifiers.GlobalModifiers[0].Multiplier * _objectGlobalModifiers.GlobalModifiers[1].Multiplier;
            Experience += experienceAddend;
            DailyIncome = _startIncome * _objectGlobalModifiers.GlobalModifiers[2].Multiplier;
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        _incomeOrEffectText.text = DailyIncome.ToString();
    }
}