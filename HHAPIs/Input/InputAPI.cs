using System;
using System.Collections.Generic;

public static class InputAPI
{
    internal static InputActionRegistry Registry;

    public static event Action<String> OnActionRegistered;

    public static IReadOnlyList<InputActionSO> AllActions
    {
        get
        {
            if (Registry == null) return Array.Empty<InputActionSO>();
            return Registry.AllActions;
        }
    }

    public static InputActionSO GetAction(string name)
    {
        if (Registry == null) return null;
        return Registry.GetAction(name);
    }

    public static bool HasAction(string name)
    {
        return Registry != null && Registry.HasAction(name);
    }

    internal static void BindRegistry(InputActionRegistry registry)
    {
        Registry = registry;
    }
}