using UnityEngine;

public class BalanceManager : MonoBehaviour
{
    [SerializeField] private float _maxCopper;
    [SerializeField] private float _maxSilver;
    private float _currentCopper;
    private float _currentSilver;
    private float _currentGold;

    public float Copper
    {
        get => _currentCopper;
        set
        {
            if (value >= _maxCopper)
            {
                _currentCopper = 0;
                _currentSilver += 1;
            }
            else
            {
                _currentCopper = value;
            }
        }
    }

    public float Silver
    {
        get => _currentSilver;
        set
        {
            _currentSilver = value;
            if (_currentSilver >= _maxSilver)
            {
                _currentSilver = 0;
                _currentGold += 1;
            }
            else if (_currentSilver <= 0)
            {
                _currentSilver = 0;
                Copper -= 1;
            }
        }
    }

    public float Gold
    {
        get => _currentGold;
        set
        {
            _currentGold = value;
            if (_currentGold <= 0)
            {
                _currentGold = 0;
                Silver -= 1;
            }
        }
    }

    public static BalanceManager Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
            return;
        }

        Singleton = this;
    }

    public float GetNetIncome()
    {
        if (GameManager.Singleton.CurrentItem)
        {
            return GameManager.Singleton.CurrentJob.DailyIncome - GameManager.Singleton.CurrentItem.Expense;;
        }
        else
        {
            return GameManager.Singleton.CurrentJob.DailyIncome;
        }
    }
}