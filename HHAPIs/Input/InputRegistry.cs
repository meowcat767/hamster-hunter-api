using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputActionRegistry", menuName = "Input System/Input Action Registry", order = 1)]
public class InputActionRegistry : ScriptableObject
{
    private const string REGISTRY_PATH = "InputActionRegistry";

    [SerializeField]
    private List<InputActionSO> _actions;

    private Dictionary<string, InputActionSO> _lookupCache;

    private static InputActionRegistry _instance;

    public static InputActionRegistry Instance => null;

    public IReadOnlyList<InputActionSO> AllActions => null;

    public InputActionSO GetAction(string actionName)
    {
        return null;
    }

    public bool HasAction(string actionName)
    {
        return false;
    }

    private void BuildCache()
    {
    }
}
