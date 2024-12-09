using UnityEngine;

public class GroupJob : Group
{
    [SerializeField] private float incomeModifier = 1f;
    [SerializeField] private BaseJob[] jobs;

    protected override BaseObject[] Objects => jobs;

    protected override void InitializeProgressObject(Progress newProgressObject, BaseObject baseObj)
    {
        BaseJob job = (BaseJob)baseObj;
        newProgressObject.InitializeJob(job.name, "Job", job.DailyIncome * incomeModifier, job.DailyExperience * experienceModifier);
    }
}
