using System.Collections;
using UnityEngine;

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

    public override IEnumerator IncrementXP()
    {
        while (true)
        {
            float experienceAddend = DailyExperience * _objectGlobalModifiers.GlobalModifiers[0].Multiplier * _objectGlobalModifiers.GlobalModifiers[3].Multiplier;
            Experience += experienceAddend;
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        _incomeOrEffectText.text = Effect.ID + ": " + Effect.Multiplier.ToString();
    }
}