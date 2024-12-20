using UnityEngine;
using UnityEngine.UI;

public class UIProgress : MonoBehaviour
{
    [SerializeField]
    private Progress _progress;

    [Header("UI")]
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _incomeOrEffect;
    [SerializeField]
    private Text _dailyExp;
    [SerializeField]
    private Text _expLeft;
    [SerializeField]
    private Text _level;
    [SerializeField]
    private Image _expBar;

    private void LateUpdate()
    {
        _name.text = name;
        _dailyExp.text = _progress.DailyExp.ToString();
        _expLeft.text = (_progress.MaxExp - _progress.CurrentExp).ToString();
        _level.text = _progress.Level.ToString();
        _expBar.fillAmount = Mathf.Clamp(_progress.CurrentExp / _progress.MaxExp, 0f, 1f);

        if (_progress is ProgressJob progressJob)
        {
            _incomeOrEffect.text = progressJob.DailyIncome.ToString();
        }
        else if (_progress is ProgressSkill progressSkill)
        {
            
        }
    }
}