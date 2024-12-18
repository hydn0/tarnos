using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Category
{
    private ScriptableObject[] _soObjects;
    private int _soObjectCount;

    public Category(ScriptableObject[] soObjects)
    {
        _soObjects = soObjects;
        _soObjectCount = 0;
    }

    /// <summary>
    /// Attempts to activate the next object in the sequence if requirements are met.
    /// Returns true if the current object was activated and out parameters:
    /// - activatedObject: The object that got activated.
    /// - nextRequirements: The requirements for the following object (if any).
    /// </summary>
    public bool TryActivateNextObject(out ScriptableObject activatedObject, out Requirement[] nextRequirements)
    {
        activatedObject = null;
        nextRequirements = null;

        if (_soObjectCount < _soObjects.Length)
        {
            ScriptableObject currentObject = _soObjects[_soObjectCount];

            if (currentObject is SOProgress soProgress && AreAllRequirementsMet(soProgress.Requirements))
            {
                activatedObject = currentObject;
                _soObjectCount++;

                nextRequirements = GetNextObjectRequirements();
                return true;
            }
            else if (currentObject is SOItem soItem && AreAllRequirementsMet(soItem.Requirements))
            {
                activatedObject = currentObject;
                _soObjectCount++;

                nextRequirements = GetNextObjectRequirements();
                return true;
            }
        }

        return false;
    }

    private bool AreAllRequirementsMet(IEnumerable<Requirement> requirements)
    {
        return requirements.All(requirement =>
            _soObjects
                .OfType<SOProgress>()
                .Any(soProgress => soProgress.name == requirement.ProgressReference.name && soProgress.Level >= requirement.Level));
    }

    private Requirement[] GetNextObjectRequirements()
    {
        if (_soObjectCount < _soObjects.Length)
        {
            ScriptableObject nextObject = _soObjects[_soObjectCount];
            if (nextObject is SOProgress nextProgress)
            {
                return nextProgress.Requirements;
            }
            else if (nextObject is SOItem nextItem)
            {
                return nextItem.Requirements;
            }
        }
        return null;
    }

    public Requirement[] GetCurrentRequirements()
    {
        if (_soObjectCount < _soObjects.Length)
        {
            ScriptableObject currentObject = _soObjects[_soObjectCount];
            if (currentObject is SOProgress soProgress)
            {
                return soProgress.Requirements;
            }
            else if (currentObject is SOItem soItem)
            {
                return soItem.Requirements;
            }
        }
        return null;
    }
}
