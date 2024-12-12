public class ProgressSkill : Progress
{
    public ObjectGlobalModifiers.Modifier Modifier;

    protected override void Start()
    {
        base.Start();
        _incomeOrEffectMuteText.text = "Effect";
        Selected.AddListener(_player.NewSkillActivated);
    }

    public void InitializeSkill(string progressName, string progressTag, ObjectGlobalModifiers.Modifier modifier, float dailyExperience)
    {
        name = progressName;
        tag = progressTag;
        Modifier = modifier;
        DailyExperience = dailyExperience;
        UpdateUI();
    }

    protected override float CalculateExperienceAddend()
    {
        return DailyExperience * _objectGlobalModifiers.AllExperience * _objectGlobalModifiers.SkillExperience;
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        _incomeOrEffectText.text = Modifier.Name + ": " + Modifier.Multiplier.ToString();
    }
}