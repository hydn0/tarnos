using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICategory : MonoBehaviour
{
    [SerializeField] private Text _nextRequirements;
    [SerializeField] private ScriptableObject[] _soObjects;
    [SerializeField] private GameObject _uiObject;

    private Dictionary<ScriptableObject, GameObject> _soToUi = new();
    private Category _category;

    [HideInInspector]
    public UnityEvent<Dictionary<ScriptableObject, GameObject>> UIObjectsInstantiated;

    private void Awake()
    {
        _category = new Category(_soObjects);

        List<GameObject> uiObjects = InstantiateUIObjects();
        InitializeUIObjects(uiObjects);

        UIObjectsInstantiated?.Invoke(_soToUi);

        DisplayNextRequirements(_category.GetCurrentRequirements());
    }

    private void Update()
    {
        if (_category.TryActivateNextObject(out ScriptableObject activatedObject, out Requirement[] nextRequirements))
        {
            if (_soToUi.ContainsKey(activatedObject))
            {
                _soToUi[activatedObject].SetActive(true);
            }

            DisplayNextRequirements(nextRequirements);
        }
    }

    private List<GameObject> InstantiateUIObjects()
    {
        List<GameObject> instantiatedObjects = new();
        for (int i = 0; i < _soObjects.Length; i++)
        {
            GameObject newUIObject = Instantiate(_uiObject, transform);
            newUIObject.SetActive(false);
            instantiatedObjects.Add(newUIObject);
        }
        return instantiatedObjects;
    }

    private void InitializeUIObjects(List<GameObject> uiObjects)
    {
        _soToUi.Clear();
        for (int i = 0; i < uiObjects.Count; i++)
        {
            _soToUi.Add(_soObjects[i], uiObjects[i]);
            UIObjectsInstantiated.AddListener(uiObjects[i].GetComponent<UIObject>().SetSOProgress);
        }
    }

    private void DisplayNextRequirements(Requirement[] requirements)
    {
        string nextReqText = "Required:";
        
        if (requirements != null)
        {
            foreach (Requirement requirement in requirements)
            {
                nextReqText += $" {requirement.ProgressReference.name}: {requirement.Level}";
            }
        }
        else
        {
            nextReqText = "";
        }

        _nextRequirements.text = nextReqText;
    }
}
