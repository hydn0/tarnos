using UnityEngine;

public class GroupJob : Group
{
    [SerializeField] private float incomeModifier = 1f;
    [SerializeField] private BaseJob[] jobs;

    protected override BaseObject[] Objects => jobs;

    protected override void InitializeProgressObject(Progress newProgress, BaseObject baseObj)
    {
        ProgressJob progressJob = (ProgressJob)newProgress;
        BaseJob job = (BaseJob)baseObj;
        progressJob.InitializeJob(job.name, "Job", job.DailyIncome * incomeModifier, job.DailyExperience * experienceModifier);
    }
}
