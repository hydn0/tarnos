using UnityEngine;

public class JobGroup : MonoBehaviour
{
    [SerializeField] private ProgressObject[] jobs;
    [SerializeField] private float incomeModifier;
    [SerializeField] private float experienceModifier;

    private void SetTag()
    {
        foreach (ProgressObject job in jobs)
        {
            job.tag = "Job";
        }
    }

    private void ApplyModifiers()
    {
        foreach (ProgressObject job in jobs)
        {
            job.DailyIncome *= incomeModifier;
            job.DailyExperience *= experienceModifier;
        }
    }
}
