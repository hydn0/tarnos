using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private RequirementProgress[] _requirements;

    public RequirementProgress[] Requirements => _requirements;
}