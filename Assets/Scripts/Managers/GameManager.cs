using System;
using System.Collections;
using System.Reflection;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ObjectGlobalModifiers _objectGlobalModifiers;
    private ProgressJob _currentJob;
    private ProgressSkill _currentSkill;
    private Item _currentItem;
    private bool _isPaused;
    private int _day;
    private int _age;

    public bool IsPaused
    {
        get => _isPaused;
        private set
        {
            _isPaused = value;
            if (_isPaused)
            {
                StopAllCoroutines();
                var allProgressObjects = GameObject.FindGameObjectsWithTag("Job")
                    .Concat(GameObject.FindGameObjectsWithTag("Skill"));
                foreach (GameObject gameObject in allProgressObjects)
                {
                    Progress progress = gameObject.GetComponent<Progress>();
                    progress.StopAllCoroutines();
                }
            }
            else
            {
                StartCoroutine(IncrementDay());
                if (_currentJob != null)
                {
                    _currentJob.StartCoroutine(_currentJob.IncrementXP());
                }
                if (_currentSkill != null)
                {
                    _currentSkill.StartCoroutine(_currentSkill.IncrementXP());
                }
            }
        }
    }

    public int Day
    {
        get => _day;
        private set
        {
            if (_day >= 365)
            {
                _day = 0;
                _age += 1;
            }
            else
            {
                _day = value;
            }
        }
    }

    public static GameManager Singleton { get; private set; }
    public int Age => _age;
    public ProgressJob CurrentJob => _currentJob;
    public ProgressSkill CurrentSkill => _currentSkill;
    public Item CurrentItem => _currentItem;

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
        ResetAllGlobalModifiers();
        _currentJob = GameObject.FindWithTag("Job")?.GetComponent<Progress>() as ProgressJob;
        _currentSkill = GameObject.FindWithTag("Skill")?.GetComponent<Progress>() as ProgressSkill;
        IsPaused = false;
    }

    public void NewProgressClicked(Progress progress)
    {
        if (progress is ProgressSkill skill)
        {
            if (_currentSkill != null)
            {
                _currentSkill.StopAllCoroutines();
            }
            _currentSkill = skill;
            if (!_isPaused)
            {
                _currentSkill.StartCoroutine(_currentSkill.IncrementXP());
            }
        }
        else if (progress is ProgressJob job)
        {
            if (_currentJob != null)
            {
                _currentJob.StopAllCoroutines();
            }
            _currentJob = job;
            if (!_isPaused)
            {
                _currentJob.StartCoroutine(_currentJob.IncrementXP());
            }
        }
    }

    public void NewItemClicked(Item newItem)
    {
        _currentItem = newItem;
    }


    public void TogglePause(bool pause)
    {
        IsPaused = pause;
    }

    private IEnumerator IncrementDay()
    {
        while (_age <= 70)
        {
            Day += 1;
            yield return new WaitForSeconds(1f);
        }
    }

    private void ResetAllGlobalModifiers()
    {
        Type globalModifiersClass = _objectGlobalModifiers.GetType();
        foreach (FieldInfo fieldInfo in globalModifiersClass.GetFields())
        {
            if (fieldInfo.FieldType == typeof(float))
            {
                fieldInfo.SetValue(_objectGlobalModifiers, 1f);
            }
        }
    }
}
