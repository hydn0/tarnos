using UnityEngine;

[CreateAssetMenu(fileName = "ObjectItem", menuName = "ScriptableObjects/ObjectItem")]
public class ObjectItem : ScriptableObject
{
    public ObjectGlobalModifiers.Modifier Effect;
    public float Expense;
}
