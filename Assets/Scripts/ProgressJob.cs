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

    protected override float CalculateExperienceAddend()
    {
        return DailyExperience * _objectGlobalModifiers.AllExperience * _objectGlobalModifiers.JobExperience;
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        DailyIncome = _startIncome * _objectGlobalModifiers.JobIncome;
        _incomeOrEffectText.text = DailyIncome.ToString();
    }
}