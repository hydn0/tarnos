using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public abstract class Progress : MonoBehaviour
{
    public float DailyExperience = 1f;

    [Header("UI")]
    [SerializeField] protected TextMeshProUGUI _incomeOrEffectText;
    [SerializeField] protected TextMeshProUGUI _incomeOrEffectMuteText;
    [SerializeField] protected Image _experienceProgressBar;
    [SerializeField] protected TextMeshProUGUI _dailyExperienceText;
    [SerializeField] protected TextMeshProUGUI _experienceLeftText;
    [SerializeField] protected TextMeshProUGUI _levelText;

    [SerializeField] protected ObjectGlobalModifiers _objectGlobalModifiers;
    protected float _experience;

    public UnityEvent<Progress> LeveledUp = new();

    public float Level { get; protected set; }
    public float Experience
    {
        get => _experience;
        protected set
        {
            if (value >= MaxExperience)
            {
                _experience = 0f;
                MaxExperience += 1f;
                Level += 1;
                LevelUp();
            }
            else
            {
                _experience = value;
            }
        }
    }
    public float MaxExperience { get; protected set; } = 5f;

    protected abstract void Start();

    public void OnClick()
    {
        GameManager.Singleton.NewProgressClicked(this);
    }

    public virtual IEnumerator IncrementXP()
    {
        while (true)
        {
            float experienceAddend = CalculateExperienceAddend();
            Experience += experienceAddend;
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    protected abstract float CalculateExperienceAddend();

    protected virtual void UpdateUI()
    {
        if (transform.Find("NameText") != null)
        {
            transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;
        }

        _dailyExperienceText.text = DailyExperience.ToString();
        _experienceProgressBar.fillAmount = Mathf.Clamp(Experience / MaxExperience, 0f, 1f);
        _experienceLeftText.text = (MaxExperience - _experience).ToString();
        _levelText.text = Level.ToString();
    }

    private void LevelUp()
    {
        LeveledUp.Invoke(this);
    }
}