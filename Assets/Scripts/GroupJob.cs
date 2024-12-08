using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupJob : Group
{
    [SerializeField] private float incomeModifier = 1f;
    [SerializeField] private BaseJob[] jobs;

    void Awake()
    {
        InstantiateJobs();
        DisplayNextRequirements(jobs[progressObjectCount].Requirements);
    }

    public void OnJobLeveledUp()
    {
        if (progressObjectCount < jobs.Length && AreAllRequirementsMet(jobs[progressObjectCount].Requirements))
        {
            ActivateJobObjects(jobs[progressObjectCount].name);
            progressObjectCount += 1;
            if (progressObjectCount < jobs.Length)
            {
                DisplayNextRequirements(jobs[progressObjectCount].Requirements);
            }
            else
            {
                nextRequirementsText.text = "";
            }
        }
    }

    private bool AreAllRequirementsMet(IEnumerable<BaseObject.Requirement> requirements)
    {
        return requirements.All(r => 
            progressObjects.Any(o => o.name == r.Object.name && o.Level == r.Level));
    }

    private void ActivateJobObjects(string jobName)
    {
        foreach (var obj in progressObjects.Where(o => o.name == jobName))
        {
            obj.gameObject.SetActive(true);
        }
    }

    private void DisplayNextRequirements(List<BaseObject.Requirement> requirements)
    {
        string nextRequirements = "Required:";
        foreach (BaseObject.Requirement requirement in requirements)
        {
            nextRequirements += " " + requirement.Object.name + ": " + requirement.Level;
        }
        nextRequirementsText.text = nextRequirements;
    }

    private void InstantiateJobs()
    {
        List<Progress> jobObjects = new();

        for (int i = 0; i < jobs.Length; i++)
        {
            Progress newJobObject = Instantiate(progressObject, panel.transform);
    
            BaseJob job = jobs[i];
            newJobObject.InitializeJob(job.name, "Job", job.DailyIncome * incomeModifier, job.DailyExperience * experienceModifier);

            newJobObject.LeveledUp.AddListener(OnJobLeveledUp);
            progressObjects.Add(newJobObject);
            if (i != 0)
            {
                newJobObject.gameObject.SetActive(false);
            }
        }
    }
}
