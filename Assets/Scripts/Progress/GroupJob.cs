using UnityEngine;

public class GroupJob : Group
{
    [SerializeField] private ObjectGlobalModifiers _objectGlobalModifiers;
    [SerializeField] private float incomeModifier = 1f;
    [SerializeField] private ObjectProgressJob[] objectJobs;

    protected override ObjectProgress[] Objects => objectJobs;

    protected override void InitializeProgressObject(Progress newProgress, ObjectProgress progressObj)
    {
        ProgressJob progressJob = (ProgressJob)newProgress;
        ObjectProgressJob job = (ObjectProgressJob)progressObj;
        progressJob.InitializeJob(job.name, "Job", job.DailyIncome * incomeModifier, job.DailyExperience * experienceModifier);
    }

    protected override void ScaleProgress(Progress progress)
    {
        ProgressJob progressJob = (ProgressJob)progress;
        foreach (ObjectProgressJob objectProgressJob in objectJobs)
        {
            if (progressJob.name == objectProgressJob.name)
            {
                foreach (Vector2 curveYAndMultiplier in objectProgressJob.IncomeScaling.curveYAndMultiplier)
                {
                    float normalizedLevel = progressJob.Level / objectProgressJob.IncomeScaling.MaxLevel;
                    float curveY = objectProgressJob.IncomeScaling.Curve.Evaluate(normalizedLevel);
                    if (Mathf.Approximately(curveYAndMultiplier.x, curveY))
                    {
                        float multiplier = _objectGlobalModifiers.JobIncome * curveYAndMultiplier.y;
                        progressJob.DailyIncome *= multiplier;
                    }
                }
            }
        }
    }
}
