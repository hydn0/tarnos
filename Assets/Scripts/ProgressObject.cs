using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ProgressObject : MonoBehaviour
{
    public float DailyIncome;
    public float DailyExperience;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _incomeText;
    [SerializeField] private Image _experienceProgressBar;
    [SerializeField] private TextMeshProUGUI _experienceLeftText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [Header("State")]
    private float _experience;
    private float _maxExperience = 5f;

    public float Level
    {
        get;
        private set;
    }
    private float Experience
    {
        get => _experience;
        set
        {
            _experience = value;
            if (value >= _maxExperience)
            {
                _experience = 0f;
                _maxExperience += 1f;
                Level += 1;
                LeveledUp.Invoke();
            }
        }
    }

    public UnityEvent<ProgressObject> Selected = new();
    public UnityEvent LeveledUp = new();

    void Start()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Selected.AddListener(player.NewJobActivated);
    }

    public void Initialize(string objectName, string objectTag, float dailyIncome, float dailyExperience)
    {
        name = objectName;
        tag = objectTag;
        DailyIncome = dailyIncome;
        DailyExperience = dailyExperience;
        UpdateUI();
    }

    public void OnClick()
    {
        Selected.Invoke(this);
    }

    public IEnumerator IncrementXP()
    {
        while (true)
        {
            Experience += DailyExperience;
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateUI()
    {
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;
        _incomeText.text = DailyIncome.ToString();
        _experienceProgressBar.fillAmount = Mathf.Clamp(Experience / _maxExperience, 0f, 1f);
        _experienceLeftText.text = (_maxExperience - _experience).ToString();
        _levelText.text = Level.ToString();
    }
}
