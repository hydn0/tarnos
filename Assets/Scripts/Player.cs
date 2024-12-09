using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI ageText;
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private TextMeshProUGUI pauseToggleText;
    [SerializeField] private TextMeshProUGUI netBalanceText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI expenseText;
    [SerializeField] private TextMeshProUGUI currentJobText;
    [SerializeField] private TextMeshProUGUI currentSkillText;
    [SerializeField] private Image currentJobProgressBar;
    [SerializeField] private Image currentSkillProgressBar;
    [Header("State")]
    private ProgressJob _currentJob;
    private ProgressSkill _currentSkill;
    private bool _isPaused;
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

                var allProgressObjects = GameObject.FindGameObjectsWithTag("Job")
                .Concat(GameObject.FindGameObjectsWithTag("Skill"));
                foreach (GameObject gameObject in allProgressObjects)
                {
                    Progress progress = gameObject.GetComponent<Progress>();
                    progress.StopAllCoroutines();
                }
            }
            else
            {
                StartCoroutine(IncrementDay());
                if (_currentJob)
                {
                    _currentJob.StartCoroutine(_currentJob.IncrementXP());
                }
                if (_currentSkill)
                {
                    _currentSkill.StartCoroutine(_currentSkill.IncrementXP());
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
        _currentJob = GameObject.FindWithTag("Job").GetComponent<Progress>() as ProgressJob;
        _currentSkill = GameObject.FindWithTag("Skill").GetComponent<Progress>() as ProgressSkill;
        IsPaused = false;
    }

    public void NewJobActivated(Progress job)
    {
        if (_currentJob)
        {
            _currentJob.StopAllCoroutines();
        }
        _currentJob = (ProgressJob)job;
        if (!IsPaused)
        {
            _currentJob.StartCoroutine(_currentJob.IncrementXP());
        }
    }

    public void NewSkillActivated(Progress skill)
    {
        if (_currentSkill)
        {
            _currentSkill.StopAllCoroutines();
        }
        _currentSkill = (ProgressSkill)skill;
        if (!IsPaused)
        {
            _currentSkill.StartCoroutine(_currentSkill.IncrementXP());
        }
    }

    public void TogglePause(Toggle toggle)
    {
        if (toggle.isOn)
        {
            pauseToggleText.text = "Play";
            IsPaused = true;
        }
        else
        {
            pauseToggleText.text = "Pause";
            IsPaused = false;
        }
    }

    private IEnumerator IncrementDay()
    {
        while (_age <= 70)
        {
            Day += 1;
            UpdateUIText();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateUIText()
    {
        dayText.text = "Day: " + Day;
        ageText.text = "Age: " + _age;
        balanceText.text = _balance.ToString();
        netBalanceText.text = "Net/day: ";
        incomeText.text = "Income/day: " + _currentJob.DailyIncome;
        expenseText.text = "Expense/day: ";

        currentJobProgressBar.fillAmount = Mathf.Clamp(_currentJob.Experience / _currentJob.MaxExperience, 0f, 1f);
        currentJobText.text = _currentJob.name;
        currentSkillProgressBar.fillAmount = Mathf.Clamp(_currentSkill.Experience / _currentSkill.MaxExperience, 0f, 1f);
        currentSkillText.text = _currentSkill.name;
    }
}
