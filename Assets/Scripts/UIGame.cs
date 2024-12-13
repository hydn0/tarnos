using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGame : MonoBehaviour
{
    [Header("UI References")]
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


    private void LateUpdate()
    {
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        dayText.text = "Day: " + GameManager.Singleton.Day;
        ageText.text = "Age: " + GameManager.Singleton.Age;
        balanceText.text = GetBalance();
        netBalanceText.text = "Net/day: " + BalanceManager.Singleton.GetNetIncome();

        if (GameManager.Singleton.CurrentJob != null)
        {
            incomeText.text = "Income/day: " + GameManager.Singleton.CurrentJob.DailyIncome;
            currentJobProgressBar.fillAmount = Mathf.Clamp(GameManager.Singleton.CurrentJob.Experience / GameManager.Singleton.CurrentJob.MaxExperience, 0f, 1f);
            currentJobText.text = GameManager.Singleton.CurrentJob.name;
        }
        else
        {
            incomeText.text = "Income/day: N/A";
            currentJobText.text = "No Job";
        }

        expenseText.text = "Expense/day: ";

        if (GameManager.Singleton.CurrentSkill != null)
        {
            currentSkillProgressBar.fillAmount = Mathf.Clamp(GameManager.Singleton.CurrentSkill.Experience / GameManager.Singleton.CurrentSkill.MaxExperience, 0f, 1f);
            currentSkillText.text = GameManager.Singleton.CurrentSkill.name;
        }
        else
        {
            currentSkillText.text = "No Skill";
        }

        pauseToggleText.text = GameManager.Singleton.IsPaused ? "Play" : "Pause";
    }

    private string GetBalance()
    {
        string balance = "";
        if (BalanceManager.Singleton.Copper >= 1)
        {
            balance += $"<color=#FFA500>{BalanceManager.Singleton.Copper}c</color>";
        }
        if (BalanceManager.Singleton.Silver >= 1)
        {
            balance += $"<color=#C0C0C0>{BalanceManager.Singleton.Silver}s</color>";
        }
        if (BalanceManager.Singleton.Gold >= 1)
        {
            balance += $"<color=#FFD700>{BalanceManager.Singleton.Gold}s</color>";
        }
        return balance;
    }

    public void OnTogglePause(Toggle toggle)
    {
        GameManager.Singleton.TogglePause(toggle.isOn);
    }
}
