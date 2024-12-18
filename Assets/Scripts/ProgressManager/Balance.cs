using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField]
    private float _maxCopper = 100;
    [SerializeField]
    private float _maxSilver = 10;

    private float _currentCopper;
    private float _currentSilver;
    private float _currentGold;

    public float Copper
    {
        get => _currentCopper;
        set => UpdateBalance(ref _currentCopper, ref _currentSilver, value, _maxCopper);
    }

    public float Silver
    {
        get => _currentSilver;
        set => UpdateBalance(ref _currentSilver, ref _currentGold, value, _maxSilver);
    }

    public float Gold
    {
        get => _currentGold;
        set => _currentGold = Mathf.Max(value, 0f);
    }

    private void Start()
    {
        
    }

    public IEnumerator IncrementBalance(float dailyIncome, Dictionary<Effect.ID, float> effects)
    {
        while (true)
        {
            Copper += dailyIncome * effects[Effect.ID.JobIncome];
            yield return new WaitForSeconds(effects[Effect.ID.GameSpeed]);
        }
    }

    private void UpdateBalance(ref float current, ref float next, float value, float max)
    {
        if (value >= max)
        {
            current = 0;
            next += 1;
        }
    }
}
