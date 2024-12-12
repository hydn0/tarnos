public class ProgressJob : Progress
{
    public float DailyIncome;

    protected override void Start()
    {
        base.Start();
        _incomeOrEffectMuteText.text = "Income";
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