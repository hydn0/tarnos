using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CategoryRequirement : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _instanceObjects;
    private int _activeCount;

    private void Start()
    {
        InstantiateObjects();
    }

    private void FixedUpdate()
    {
        if (_activeCount < _instanceObjects.Length && TryActivateNextObject())
        {
            _activeCount++;
        }
    }

    public RequirementProgress[] GetNextRequirements()
    {
        if (_activeCount < _instanceObjects.Length)
        {
            GameObject nextObj = _instanceObjects[_activeCount];

            if (nextObj.GetComponent<Progress>() is Progress progress)
            {
                return progress.Requirements;
            }
            else if (nextObj.GetComponent<Item>() is Item item)
            {
                return item.Requirements;
            }
        }
        return null;
    }

    private bool TryActivateNextObject()
    {
        GameObject nextObj = _instanceObjects[_activeCount];

        if ((nextObj.GetComponent<Progress>() is Progress progress && AreAllRequirementsMet(progress.Requirements))
            || (nextObj.GetComponent<Item>() is Item item && AreAllRequirementsMet(item.Requirements)))
        {
            nextObj.SetActive(true);
            return true;
        }
    
        return false;
    }


    private bool AreAllRequirementsMet(RequirementProgress[] requirements)
    {
        return GetAllProgressComponents().All(progressComponent =>
            requirements
            .Any(requirement => requirement.ProgressID == progressComponent.RequirementID && requirement.Level <= progressComponent.Level));
    }

    private IEnumerable<Progress> GetAllProgressComponents()
    {
        return GameObject.FindGameObjectsWithTag("Progress")
            .Select(obj => obj.GetComponent<Progress>())
            .Where(progressComponent => progressComponent != null);
    }
    
    private void InstantiateObjects()
    {
        List<GameObject> instances = new();

        foreach (GameObject obj in _instanceObjects)
        {
            GameObject newObj = Instantiate(obj, transform);
            newObj.SetActive(false);
            instances.Add(newObj);
        }
    
        _instanceObjects = instances.ToArray();
    }
}
