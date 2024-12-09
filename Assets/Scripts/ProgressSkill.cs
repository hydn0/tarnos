public class ProgressSkill : Progress
{
    public ObjectProgressSkill.Modifier Effect { get; private set; }

    protected override void Start()
    {
        base.Start();
        _incomeOrEffectMuteText.text = "Effect";
        Selected.AddListener(_player.NewSkillActivated);
    }

    public void InitializeSkill(string progressName, string progressTag, ObjectProgressSkill.Modifier effect, float dailyExperience)
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