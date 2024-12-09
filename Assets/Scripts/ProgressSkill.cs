public class ProgressSkill : Progress
{
    private ObjectModifiers.Modifier _effect;

    protected override void Start()
    {
        base.Start();
        _incomeOrEffectMuteText.text = "Effect";
        Selected.AddListener(_player.NewSkillActivated);
    }

    public void InitializeSkill(string progressName, string progressTag, ObjectModifiers.Modifier effect, float dailyExperience)
    {
        name = progressName;
        tag = progressTag;
        _effect = effect;
        DailyExperience = dailyExperience;
        UpdateUI();
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        _incomeOrEffectText.text = _effect.Multiplier.ToString();
    }
}