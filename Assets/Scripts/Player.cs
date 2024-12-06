using System.Collections;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI ageText;
    [SerializeField] private TextMeshProUGUI balanceText;
    [Header("State")]
    private ProgressObject _activeJob;
    private bool isPaused;
    private int _day;
    private int _age;
    private float _balance;

    private int day
    {
        get => _day;
        set
        {
            if (value >= 365)
            {
                _day = 0;
                _age += 1;
            }
            else
            {
                _day = value;
                _balance += _activeJob.DailyIncome;
            }
        }
    }

    void Start()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Job"))
        {
            ProgressObject progressObject = gameObject.GetComponent<ProgressObject>();
            progressObject.Selected.AddListener(NewJobActivated);
        }
    }

    public void NewJobActivated(ProgressObject job)
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Job"))
        {
            ProgressObject progressObject = gameObject.GetComponent<ProgressObject>();
            progressObject.StopAllCoroutines();
            
        }

        _activeJob = job;
        StartCoroutine(_activeJob.IncrementXP());
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            isPaused = false;
            StartCoroutine(_activeJob.IncrementXP());
        }
        else
        {
            isPaused = true;
            StopCoroutine(_activeJob.IncrementXP());
        }
    }

    private IEnumerator IncrementDay()
    {
        while (true)
        {
            day += 1;
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateUI()
    {
        dayText.text = "Day: " + _day;
        ageText.text = "Age: " + _age;
        balanceText.text = "Balance: " + _balance;
    }
}
