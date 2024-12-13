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
                _currentCopper = 0f;
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
            if (value >= _maxSilver)
            {
                _currentSilver = 0f;
                _currentGold += 1;
            }
            else
            {
                _currentSilver = value;
            }
        }
    }

    public float Gold => _currentGold;
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

}