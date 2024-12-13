using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ObjectItem[] _objectItems;
    [SerializeField] private Item item;
    [SerializeField] private GameObject panel;

    private void Awake()
    {
        InstantiateObjects();
    }

    private void InstantiateObjects()
    {
        for (int i = 0; i < _objectItems.Length; i++)
        {
            Item newObject = Instantiate(item, panel.transform);
            newObject.InitializeItem(_objectItems[i].name, _objectItems[i].Effect, _objectItems[i].Expense);
        }
    }
}
