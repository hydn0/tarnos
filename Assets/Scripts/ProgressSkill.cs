public class ProgressSkill : Progress
{
    public ObjectGlobalModifiers.Modifier Effect;

    protected override void Start()
    {
        base.Start();
        _incomeOrEffectMuteText.text = "Effect";
        Selected.AddListener(_player.NewSkillActivated);
    }

    public void InitializeSkill(string progressName, string progressTag, ObjectGlobalModifiers.Modifier effect, float dailyExperience)
    {
        name = progressName;
        tag = progressTag;
        Effect = effect;
        DailyExperience = dailyExperience;
        UpdateUI();
    }

    protected override float CalculateExperience()
    {
        return DailyExperience * _objectGlobalModifiers.GlobalModifiers[0].Multiplier * _objectGlobalModifiers.GlobalModifiers[3].Multiplier;
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        _incomeOrEffectText.text = Effect.ID + ": " + Effect.Multiplier.ToString();
    }
}