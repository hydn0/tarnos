using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class JobGroup : MonoBehaviour
{
    [SerializeField] private BaseJob[] jobs;
    [SerializeField] private float incomeModifier = 1f;
    [SerializeField] private float experienceModifier = 1f;
    [SerializeField] private ProgressObject progressObject;
    private List<ProgressObject> _jobObjects = new();
    private int _jobCount = 1;
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI nextRequirementsText;

    void Awake()
    {
        InstantiateJobs();
        DisplayNextRequirements(jobs[_jobCount].Requirements);
    }

    public void OnJobLeveledUp()
    {
        if (_jobCount < jobs.Length && AreAllRequirementsMet(jobs[_jobCount].Requirements))
        {
            ActivateJobObjects(jobs[_jobCount].name);
            _jobCount += 1;
            if (_jobCount < jobs.Length)
            {
                DisplayNextRequirements(jobs[_jobCount].Requirements);
            }
            else
            {
                nextRequirementsText.text = "";
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

    private void DisplayNextRequirements(List<BaseJob.Requirement> requirements)
    {
        string nextRequirements = "Required:";
        foreach (BaseJob.Requirement requirement in requirements)
        {
            nextRequirements += " " + requirement.Job.name + ": " + requirement.Level;
        }
        nextRequirementsText.text = nextRequirements;
    }

    private void InstantiateJobs()
    {
        List<ProgressObject> jobObjects = new();

        for (int i = 0; i < jobs.Length; i++)
        {
            ProgressObject newJobObject = Instantiate(progressObject, panel.transform);
    
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
