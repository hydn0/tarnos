using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [SerializeField]
    private SOProgress[] CommonWork;
    [SerializeField]
    private SOProgress[] Military;
    [SerializeField]
    private SOProgress[] Fundamentals;
    [SerializeField]
    private SOProgress[] Combat;
    [SerializeField]
    private SOProgress[] Magic;

    private EXP _exp;
    private Balance _balance;
    private Effect _effect;

    public SOProgressJob CurrentJob { get; private set; }
    public SOProgressSkill CurrentSkill { get; private set; }
    public SOItem CurrentItem { get; private set; }
    public static ProgressManager Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;

        _exp = GetComponent<EXP>();
        _balance = GetComponent<Balance>();
        _effect = GetComponent<Effect>();

        _exp.ResetProgressEXP(CommonWork);
        _exp.ResetProgressEXP(Military);
        _exp.ResetProgressEXP(Fundamentals);
        _exp.ResetProgressEXP(Combat);
        _exp.ResetProgressEXP(Magic);
        _effect.ResetEffects();

        CurrentJob = (SOProgressJob)CommonWork[0];
        CurrentSkill = (SOProgressSkill)Fundamentals[0];
    }

    public void SetCurrentSO(ScriptableObject scriptableObject)
    {
        if (scriptableObject is SOProgressJob soProgressJob)
        {
            CurrentJob = soProgressJob;
        }
        else if (scriptableObject is SOProgressSkill soProgressSkill)
        {
            CurrentSkill = soProgressSkill;
        }
        else if (scriptableObject is SOItem soItem)
        {
            CurrentItem = soItem;
        }
    }
}