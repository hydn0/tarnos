using System;
using System.Reflection;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public ObjectGlobalModifiers.Modifier Modifier;
    public float Expense;
    [SerializeField] private ObjectGlobalModifiers _objectGlobalModifiers;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _effectText;
    [SerializeField] private TextMeshProUGUI _expenseText;

    public void InitializeItem(string itemName, ObjectGlobalModifiers.Modifier modifier, float expense)
    {
        _nameText.text = itemName;
        Modifier = modifier;
        Expense = expense;
        _effectText.text = Modifier.Name + ": " + Modifier.Multiplier.ToString();
        _expenseText.text = Expense.ToString();
    }

    public void OnClick()
    {
        GameManager.Singleton.NewItemClicked(this);
        ApplyModifierToGlobal();
    }

    private void ApplyModifierToGlobal()
    {
        Type globalModifiersClass = typeof(ObjectGlobalModifiers);
        foreach (FieldInfo fieldInfo in globalModifiersClass.GetFields())
        {
            if (fieldInfo.Name == Modifier.Name.ToString())
            {
                float currentValue = (float)fieldInfo.GetValue(_objectGlobalModifiers);
                fieldInfo.SetValue(_objectGlobalModifiers, currentValue + Modifier.Multiplier);
            }
        }
    }
}
