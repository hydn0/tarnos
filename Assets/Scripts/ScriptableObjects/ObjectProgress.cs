using System.Collections.Generic;
using UnityEngine;

public class ObjectProgress : ScriptableObject
{
    public float DailyExperience = 1f;
    public List<Requirement> Requirements;

    [System.Serializable]
    public struct Requirement
    {
        public ObjectProgress Object;
        public int Level;
    }
}