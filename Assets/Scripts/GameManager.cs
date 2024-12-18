using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private bool _isPaused;
    private int _day;

    public bool IsPaused
    {
        get =>_isPaused;
        set
        {
            if (value)
            {
                StopAllCoroutines();
            }
            else
            {
                StartCoroutine(IncrementDay());
            }
            _isPaused = value;
        }
    }

    public int Day
    {
        get => _day;
        private set
        {
            if (value >= 365)
            {
                _day = 0;
                Age += 1;
            }
            else
            {
                _day = value;
            }
        }
    }

    [SerializeField]
    private UnityEvent DayIncreased;

    public int Age { get; private set; }
    public static GameManager Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;        
    }

    private void Start()
    {
        IsPaused = false;
    }

    private IEnumerator IncrementDay()
    {
        while (true)
        {
            Day += 1;
            DayIncreased?.Invoke();
            yield return new WaitForSeconds(1);
        }
    }
}