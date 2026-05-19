using System;
using System.Collections.Generic;

public static class InputAPI
{
    internal static InputActionRegistry Registry;

    public static bool Ready => Registry != null;

    public static void BindRegistry(InputActionRegistry registry)
    {
        Registry = registry;
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

    public static IReadOnlyList<InputActionSO> AllActions =>
        Registry?.AllActions;
}