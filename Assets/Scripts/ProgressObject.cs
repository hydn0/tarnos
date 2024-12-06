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
    private float _maxExperience = 100f;
    private float _level;

    private float experience
    {
        get => _experience;
        set
        {
            if (value >= _maxExperience)
            {
                _experience = 0f;
                _maxExperience += 100f;
                _level += 1;
            }
            else
            {
                _experience = value;
            }
        }
    }

    public UnityEvent<ProgressObject> Selected = new();

    public void OnClick()
    {
        Selected.Invoke(this);
    }

    public IEnumerator IncrementXP()
    {
        while (true)
        {
            experience += DailyExperience;
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateUI()
    {
        _incomeText.text = DailyIncome.ToString();
        _experienceProgressBar.fillAmount = Mathf.Clamp(experience / _maxExperience, 0f, 1f);
        _experienceLeftText.text = (_maxExperience - _experience).ToString();
        _levelText.text = _level.ToString();
    }
}
