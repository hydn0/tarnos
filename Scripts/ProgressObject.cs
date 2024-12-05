using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ProgressObject : MonoBehaviour
{
    private static List<ProgressObject> Instances = new();
    public int Income;
    private Image _progressBar;
    private TextMeshProUGUI _levelText;
    private TextMeshProUGUI _xpLeftText;
    private int _xp = 0;
    private int _xpMax = 10;
    private int _level = 0;

    public int XP
    {
        get => _xp;
        set
        {
            if (value >= _xpMax)
            {
                _xp = 0;
                _xpMax = 10;
                _progressBar.fillAmount = _xp;
                _level += 1;
                _levelText.text = _level.ToString();
            }
            else
            {
                _xp = value;
                _progressBar.fillAmount = Mathf.Clamp((float)_xp / _xpMax, 0f, 1f);
                _xpLeftText.text = (_xpMax - _xp).ToString();
            }
        }
    }

    private UnityEvent<ProgressObject> activated = new();

    void Start()
    {
        Instances.Add(this);
    
        _progressBar = transform.Find("ProgressBar").GetComponent<Image>();
        _levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        _xpLeftText = transform.Find("XPLeftText").GetComponent<TextMeshProUGUI>();
        transform.Find("IncomeText").GetComponent<TextMeshProUGUI>().text = Income.ToString();
        
        activated.AddListener(Player.Singleton.NewObjectActivated);
    }

    public void OnClick()
    {
        activated.Invoke(this);
    }

    public void StartIncrement(bool isPlaying)
    {
        if (isPlaying)
        {
            StartCoroutine(IncrementXP());
        }
        else
        {
            StopAllIncrements();
        }
    }

    private IEnumerator IncrementXP()
    {
        while (true)
        {
            XP += 1;
            yield return new WaitForSeconds(1f);
        }
    }

    private void StopAllIncrements()
    {
        foreach (MonoBehaviour instance in Instances)
        {
            instance.StopAllCoroutines();
        }
    }
}
