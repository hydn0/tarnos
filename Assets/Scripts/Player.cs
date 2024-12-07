using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI ageText;
    [SerializeField] private TextMeshProUGUI balanceText;
    [Header("Progress Object")]
    private List<ProgressObject> _jobs = new();
    private ProgressObject _activeJob;
    [Header("State")]
    private bool _isPaused = true;
    private int _day;
    private int _age;
    private float _balance;

    private bool isPaused
    {
        get => _isPaused;
        set
        {
            _isPaused = value;
            if (value)
            {
                StopAllCoroutines();
                foreach (ProgressObject job in _jobs)
                {
                    job.StopAllCoroutines();
                }
            }
            else
            {
                StartCoroutine(IncrementDay());
                if (_activeJob)
                {
                    _activeJob.StartCoroutine(_activeJob.IncrementXP());
                }
            }
        }
    }
    private int day
    {
        get => _day;
        set
        {
            _day = value;
            if (value >= 365)
            {
                _day = 0;
                _age += 1;
            }
            else
            {
                if (_activeJob && !isPaused)
                {
                    _balance += _activeJob.DailyIncome;
                }
            }
        }
    }

    void Start()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Job"))
        {
            ProgressObject progressObject = gameObject.GetComponent<ProgressObject>();
            _jobs.Add(progressObject);
            progressObject.Selected.AddListener(NewJobActivated);
        }
    }

    public void NewJobActivated(ProgressObject job)
    {
        if (_activeJob)
        {
            _activeJob.StopAllCoroutines();
        }
        _activeJob = job;
        if (!isPaused)
        {
            _activeJob.StartCoroutine(_activeJob.IncrementXP());
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }

    private IEnumerator IncrementDay()
    {
        while (_age <= 70)
        {
            day += 1;
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateUI()
    {
        dayText.text = "Day: " + day;
        ageText.text = "Age: " + _age;
        balanceText.text = _balance.ToString();
    }
}
