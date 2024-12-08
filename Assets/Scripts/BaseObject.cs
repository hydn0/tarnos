using System.Collections.Generic;
using UnityEngine;

public class BaseObject : ScriptableObject
{
    public float DailyExperience = 1f;
    public List<Requirement> Requirements;

    [System.Serializable]
    public struct Requirement
    {
        public BaseObject Object;
        public int Level;
    }
}