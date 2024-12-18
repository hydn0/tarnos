using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    [SerializeField]
    private Text _day;
    [SerializeField]
    private Text _age;
    [SerializeField]
    private Text _balance;
    [SerializeField]
    private Text _pauseToggle;
    [SerializeField]
    private Text _netBalance;
    [SerializeField]
    private Text _income;
    [SerializeField]
    private Text _expense;
    [SerializeField]
    private Text _currentJob;
    [SerializeField]
    private Text _currentSkill;
    [SerializeField]
    private Image _currentJobExpBar;
    [SerializeField]
    private Image _currentSkillExpBar;

    [SerializeField]
    private UnityEvent<bool> TogglePause;

    private void LateUpdate()
    {
        _day.text = "Day: " + GameManager.Singleton.Day;
        _age.text = "Age: " + GameManager.Singleton.Age;
        _balance.text = GetFormattedBalance();


        _income.text = "Income/day: " + ProgressManager.Singleton.CurrentJob.DailyIncome;
        _currentJobExpBar.fillAmount = Mathf.Clamp(ProgressManager.Singleton.CurrentJob.CurrentExp / ProgressManager.Singleton.CurrentJob.MaxExp, 0f, 1f);
        _currentJob.text = ProgressManager.Singleton.CurrentJob.name;

        _currentSkillExpBar.fillAmount = Mathf.Clamp(ProgressManager.Singleton.CurrentSkill.CurrentExp / ProgressManager.Singleton.CurrentSkill.MaxExp, 0f, 1f);
        _currentSkill.text = ProgressManager.Singleton.CurrentSkill.name;


        if (ProgressManager.Singleton.CurrentItem)
        {
            _expense.text = "Expense/day: " + ProgressManager.Singleton.CurrentItem.DailyExpense;
            _netBalance.text = "Net/day: " + (ProgressManager.Singleton.CurrentJob.DailyIncome - ProgressManager.Singleton.CurrentItem.DailyExpense);
        }
        else
        {
            _expense.text = "Expense/day: 0";
            _netBalance.text = "Net/day: " + ProgressManager.Singleton.CurrentJob.DailyIncome;
        }
        
        _pauseToggle.text = GameManager.Singleton.IsPaused ? "Play" : "Pause";
    }

    private string GetFormattedBalance()
    {
        Balance balance = ProgressManager.Singleton.gameObject.GetComponent<Balance>();
        string formatBalance = "";
        if (balance.Copper >= 1)
        {
            formatBalance += $"<color=#FFA500>{balance.Copper}c</color>";
        }
        if (balance.Silver >= 1)
        {
            formatBalance += $"<color=#C0C0C0>{balance.Silver}s</color>";
        }
        if (balance.Gold >= 1)
        {
            formatBalance += $"<color=#FFD700>{balance.Gold}s</color>";
        }
        return formatBalance;
    }

    public void OnTogglePause(Toggle toggle)
    {
        if (toggle.isOn)
        {
            TogglePause?.Invoke(true);
        }
        else
        {
            TogglePause?.Invoke(false);
        }
    }
}