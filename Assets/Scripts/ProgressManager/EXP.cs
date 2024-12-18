using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : MonoBehaviour
{
    private Effect effect;

    private void Awake()
    {
        effect = GetComponent<Effect>();
        //ResetProgressEXP();
    }

    private void OnEnable()
    {
        StartCoroutine(IncrementEXP(ProgressManager.Singleton.CurrentJob, ProgressManager.Singleton.CurrentSkill, effect.Effects));
    }

    private IEnumerator IncrementEXP(SOProgressJob currentJob, SOProgressSkill currentSkill, Dictionary<Effect.ID, float> effects)
    {
        while (true)
        {
            currentJob.CurrentExp += currentJob.DailyExp * effects[Effect.ID.AllExperience] * effects[Effect.ID.JobExperience];
            currentSkill.CurrentExp += currentSkill.DailyExp * effects[Effect.ID.AllExperience] * effects[Effect.ID.SkillExperience];
            yield return new WaitForSeconds(effects[Effect.ID.GameSpeed]);
        }
    }

    public void ResetProgressEXP(SOProgress[] soProgresses)
    {
        foreach (SOProgress soProgress in soProgresses)
        {
            soProgress.ResetEXP();
        }
    }
}