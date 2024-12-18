using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{
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

    private ScriptableObject _soObject;

    private void LateUpdate()
    {
        if (_soObject)
        {
            _name.text = _soObject.name;
        }

        if (_soObject is SOProgressJob soProgressJob)
        {
            _incomeOrEffect.text = soProgressJob.DailyIncome.ToString();
            _dailyExp.text = soProgressJob.DailyExp.ToString();
            _expLeft.text = (soProgressJob.MaxExp - soProgressJob.CurrentExp).ToString();
            _level.text = soProgressJob.Level.ToString();
            _expBar.fillAmount = Mathf.Clamp(soProgressJob.CurrentExp / soProgressJob.MaxExp, 0f, 1f);
        }
        else if (_soObject is SOProgressSkill soProgressSkill)
        {
            _incomeOrEffect.text = soProgressSkill.Effect + "" + soProgressSkill.Multiplier.ToString();
            _dailyExp.text = soProgressSkill.DailyExp.ToString();
            _expLeft.text = (soProgressSkill.MaxExp - soProgressSkill.CurrentExp).ToString();
            _level.text = soProgressSkill.Level.ToString();
            _expBar.fillAmount = Mathf.Clamp(soProgressSkill.CurrentExp / soProgressSkill.MaxExp, 0f, 1f);
        }
        else if (_soObject is SOItem soItem)
        {
            _incomeOrEffect.text = soItem.Effect + "" + soItem.Multiplier.ToString();
        }
    }

    public void OnClick()
    {
        ProgressManager.Singleton.SetCurrentSO(_soObject);
    }

    public void SetSOProgress(Dictionary<ScriptableObject, GameObject> _soToUi)
    {
        foreach (KeyValuePair<ScriptableObject, GameObject> kvp in _soToUi)
        {
            if (kvp.Value == gameObject)
            {
                _soObject = kvp.Key;
                break;
            }
        }
    }
}