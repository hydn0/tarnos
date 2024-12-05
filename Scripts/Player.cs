using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Player : MonoBehaviour
{
    private ProgressObject _activeJob;
    private TextMeshProUGUI _dayText;
    private TextMeshProUGUI _ageText;
    private TextMeshProUGUI _balanceText;
    private int _day;
    private int _age;
    private int _balance;
    private bool _isPlaying;

    private UnityEvent<bool> playPressed = new();

    public static Player Singleton { get; private set; }
    public int Day
    {
        get => _day;
        set
        {
            if (value >= 365)
            {
                _day = 0;
                _age += 1;
            }
            else
            {
                _day = value;
            }
            _dayText.text = "Day: " + _day;
            _ageText.text = "Age: " + _age;
            _balance += _activeJob.Income;
            _balanceText.text = "Balance: " + _balance;
        }
    }

    void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
    }

    void Start()
    {
        _activeJob = FindFirstObjectByType<ProgressObject>();

        RectTransform panel = transform.Find("Panel") as RectTransform;
        _dayText = panel.Find("Day Text").GetComponent<TextMeshProUGUI>();
        _ageText = panel.Find("Age Text").GetComponent<TextMeshProUGUI>();
        _balanceText = panel.Find("Balance Text").GetComponent<TextMeshProUGUI>();
    }

    public void NewObjectActivated(ProgressObject newObject)
    {
        _activeJob.StopAllCoroutines();
        _activeJob = newObject;
        playPressed.RemoveAllListeners();
        playPressed.AddListener(isPlaying => _activeJob.StartIncrement(isPlaying));
        playPressed.Invoke(_isPlaying);
    }

    public void Play()
    {
        if (_isPlaying == false)
        {
            _isPlaying = true;
            StartCoroutine(IncrementDay());
        }
        else
        {
            _isPlaying = false;
        }
        playPressed.Invoke(_isPlaying);
    }

    private IEnumerator IncrementDay()
    {
        while (true)
        {
            Day += 1;
            yield return new WaitForSeconds(1f);
        }
    }
}
