using MelonLoader;
using UnityEngine;

public class ModMain : MelonMod
{
    public override void OnInitialzeMelon()
    {
        MelonLogger.Msg("Meowcat's Hamster Hunter APIs loading...");

        // Try to find the registry in the scene
        var registry = Object.FindFirstObjectByType<InputActionRegistry>();

        if (registry != null )
        {
            MelonLogger.Msg("[MHH] InputActionRegistry bound successfully.");
        }
        else
        {
            MelonLogger.Error("[MHH] Failed to find InputActionRegistry in the scene. API functionality will be limited.");
        }
    }
}