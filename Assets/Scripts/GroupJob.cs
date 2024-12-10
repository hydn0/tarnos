using UnityEngine;

public class GroupJob : Group
{
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
                progressJob.DailyIncome = objectProgressJob.IncomeScaling.Evaluate(progressJob.Level);
            }
        }
    }
}
