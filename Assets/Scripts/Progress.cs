using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Progress : MonoBehaviour
{
    public float DailyIncome;
    public BaseSkill.Modifier Effect;
    public float DailyExperience;
    public float MaxExperience = 5f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _incomeOrEffectText;
    [SerializeField] private TextMeshProUGUI _incomeOrEffectMuteText;
    [SerializeField] private Image _experienceProgressBar;
    [SerializeField] private TextMeshProUGUI _dailyExperienceText;
    [SerializeField] private TextMeshProUGUI _experienceLeftText;
    [SerializeField] private TextMeshProUGUI _levelText;

    private float _experience;

    public float Level { get; private set; }
    
    public float Experience
    {
        get => _experience;
        set
        {
            _experience = value;
            if (value >= MaxExperience)
            {
                _experience = 0f;
                MaxExperience += 1f;
                Level += 1;
                LevelUp();
            }
        }
    }

    public UnityEvent<Progress> Selected = new();
    public UnityEvent LeveledUp = new();

    private void Start()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (tag == "Job")
        {
            _incomeOrEffectMuteText.text = "Income";
            Selected.AddListener(player.NewJobActivated);

        }
        else if (tag == "Skill")
        {
            _incomeOrEffectMuteText.text = "Effect";
            Selected.AddListener(player.NewSkillActivated);
        }
    }

    public void InitializeJob(string objectName, string objectTag, float dailyIncome, float dailyExperience)
    {
        name = objectName;
        tag = objectTag;
        DailyIncome = dailyIncome;
        DailyExperience = dailyExperience;
        UpdateUI();
    }

    public void InitializeSkill(string objectName, string objectTag, BaseSkill.Modifier effect, float dailyExperience)
    {
        name = objectName;
        tag = objectTag;
        Effect = effect;
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
        _dailyExperienceText.text = DailyExperience.ToString();
        _experienceProgressBar.fillAmount = Mathf.Clamp(Experience / MaxExperience, 0f, 1f);
        _experienceLeftText.text = (MaxExperience - _experience).ToString();
        _levelText.text = Level.ToString();

        if (tag == "Job")
        {
            _incomeOrEffectText.text = DailyIncome.ToString();
        }
        else if (tag == "Skill")
        {
            _incomeOrEffectText.text = Effect.Multiplier.ToString();
        }
    }

    private void LevelUp()
    {
        LeveledUp.Invoke();
    }
}
