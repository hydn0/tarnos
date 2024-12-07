using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseJob", menuName = "ScriptableObjects/BaseJob")]
public class BaseJob : ScriptableObject
{
    public float DailyIncome;
    public float DailyExperience;
    public List<Requirement> Requirements;

    [System.Serializable]
    public struct Requirement
    {
        public BaseJob Job;
        public int Level;
    }
}