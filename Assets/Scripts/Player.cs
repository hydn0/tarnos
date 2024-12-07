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
    [Header("State")]
    private ProgressObject _currentJob;
    private bool _isPaused = true;
    private int _day;
    private int _age;
    private float _balance;

    private bool IsPaused
    {
        get => _isPaused;
        set
        {
            _isPaused = value;
            if (value)
            {
                StopAllCoroutines();
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Job"))
                {
                    ProgressObject progressObject = gameObject.GetComponent<ProgressObject>();
                    progressObject.StopAllCoroutines();
                }
            }
            else
            {
                StartCoroutine(IncrementDay());
                if (_currentJob)
                {
                    _currentJob.StartCoroutine(_currentJob.IncrementXP());
                }
            }
        }
    }
    private int Day
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
                if (_currentJob && !IsPaused)
                {
                    _balance += _currentJob.DailyIncome;
                }
            }
        }
    }

    void Start()
    {
        _currentJob = GameObject.FindWithTag("Job").GetComponent<ProgressObject>();
    }

    public void NewJobActivated(ProgressObject job)
    {
        if (_currentJob)
        {
            _currentJob.StopAllCoroutines();
        }
        _currentJob = job;
        if (!IsPaused)
        {
            _currentJob.StartCoroutine(_currentJob.IncrementXP());
        }
    }

    public void TogglePause()
    {
        if (IsPaused)
        {
            IsPaused = false;
        }
        else
        {
            IsPaused = true;
        }
    }

    private IEnumerator IncrementDay()
    {
        while (_age <= 70)
        {
            Day += 1;
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateUI()
    {
        dayText.text = "Day: " + Day;
        ageText.text = "Age: " + _age;
        balanceText.text = _balance.ToString();
    }
}
