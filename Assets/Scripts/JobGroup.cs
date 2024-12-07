using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JobGroup : MonoBehaviour
{
    [SerializeField] private BaseJob[] jobs;
    [SerializeField] private ProgressObject progressObject;
    [SerializeField] private float incomeModifier = 1f;
    [SerializeField] private float experienceModifier = 1f;
    private List<ProgressObject> _jobObjects = new();

    void Start()
    {
        InstantiateJobs();
    }

    public void OnJobLeveledUp()
    {
        foreach (var job in jobs)
        {
            if (AreAllRequirementsMet(job.Requirements))
            {
                ActivateJobObjects(job.name);
            }
        }
    }

    private bool AreAllRequirementsMet(IEnumerable<BaseJob.Requirement> requirements)
    {
        return requirements.All(r => 
            _jobObjects.Any(o => o.name == r.Job.name && o.Level == r.Level));
    }

    private void ActivateJobObjects(string jobName)
    {
        foreach (var obj in _jobObjects.Where(o => o.name == jobName))
        {
            obj.gameObject.SetActive(true);
        }
    }

    private void InstantiateJobs()
    {
        List<ProgressObject> jobObjects = new();

        for (int i = 0; i < jobs.Length; i++)
        {
            ProgressObject newJobObject = Instantiate(progressObject, transform);
    
            BaseJob job = jobs[i];
            newJobObject.Initialize(job.name, "Job", job.DailyIncome * incomeModifier, job.DailyExperience * experienceModifier);

            newJobObject.LeveledUp.AddListener(OnJobLeveledUp);
            _jobObjects.Add(newJobObject);
            if (i != 0)
            {
                newJobObject.gameObject.SetActive(false);
            }
        }
    }
}
