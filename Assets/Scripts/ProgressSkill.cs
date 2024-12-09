public class ProgressSkill : Progress
{
    public BaseSkill.Modifier Effect { get; private set; }

    protected override void Start()
    {
        base.Start();
        _incomeOrEffectMuteText.text = "Effect";
        Selected.AddListener(_player.NewSkillActivated);
    }

    public void InitializeSkill(string progressName, string progressTag, BaseSkill.Modifier effect, float dailyExperience)
    {
        name = progressName;
        tag = progressTag;
        Effect = effect;
        DailyExperience = dailyExperience;
        UpdateUI();
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        _incomeOrEffectText.text = Effect.Multiplier.ToString();
    }
}