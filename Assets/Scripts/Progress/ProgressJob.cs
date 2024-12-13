using System.Collections;
using UnityEngine;

public class ProgressJob : Progress
{
    public float DailyIncome;

    protected override void Start()
    {
        _incomeOrEffectMuteText.text = "Income";
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
            float experienceAddend = CalculateExperienceAddend();
            Experience += experienceAddend;
            if (GameManager.Singleton.CurrentItem)
            {
                float newIncome = DailyIncome - GameManager.Singleton.CurrentItem.Expense;
                BalanceManager.Singleton.Copper += newIncome;
            }
            else
            {
                BalanceManager.Singleton.Copper += DailyIncome;
            }
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    protected override float CalculateExperienceAddend()
    {
        return DailyExperience * _objectGlobalModifiers.AllExperience * _objectGlobalModifiers.JobExperience;
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        _incomeOrEffectText.text = DailyIncome.ToString();
    }
}