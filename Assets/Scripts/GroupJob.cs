using UnityEngine;

public class GroupJob : Group
{
    [SerializeField] private float incomeModifier = 1f;
    [SerializeField] private ObjectProgressJob[] jobs;

    protected override ObjectProgress[] Objects => jobs;

    protected override void InitializeProgressObject(Progress newProgress, ObjectProgress baseObj)
    {
        ProgressJob progressJob = (ProgressJob)newProgress;
        ObjectProgressJob job = (ObjectProgressJob)baseObj;
        progressJob.InitializeJob(job.name, "Job", job.DailyIncome * incomeModifier, job.DailyExperience * experienceModifier);
    }
}
