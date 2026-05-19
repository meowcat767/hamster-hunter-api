using UnityEngine;
using UnityEngine.XR;

[CreateAssetMenu(fileName = "New Input Action", menuName = "Input System/Input Action", order = 0)]
public class InputActionSO : ScriptableObject
{
    [Header("Action Identity")]
    [SerializeField]
    private string _actionName;

    [SerializeField]
    private InputActivationType _activationType;

    [Header("Keyboard + Mouse")]
    [SerializeField]
    private KeyCode _keyboardKey;

    [SerializeField]
    private string _keyboardLabel;

    [Header("Gamepad (Xbox / Generic)")]
    [SerializeField]
    private KeyCode _gamepadKey;

    [SerializeField]
    private string _gamepadLabel;

    [Header("PlayStation")]
    [SerializeField]
    private KeyCode _playstationKey;

    [SerializeField]
    private string _playstationLabel;


    // Read-only API (safe access)


    public string ActionName => _actionName;

    public InputActivationType ActivationType => _activationType;

    public KeyCode KeyboardKey => _keyboardKey;

    public KeyCode GamepadKey => _gamepadKey;

    public KeyCode PlayStationKey => _playstationKey;

    public string KeyboardLabel => _keyboardLabel;

    public string GamepadLabel => _gamepadLabel;

    public string PlayStationLabel => _playstationLabel;


    // Mutators (use carefully)


    public void SetKeyboardBinding(KeyCode key, string label)
    {
        _keyboardKey = key;
        _keyboardLabel = label;
    }

 
    // Device abstraction


    public KeyCode GetKeyForDevice(InputDevice device)
    {
        switch (device)
        {
            case InputDevice.KeyboardMouse:
                return _keyboardKey;

            case InputDevice.Xbox:
                return _gamepadKey;

            case InputDevice.PlayStation:
                return _playstationKey;

            default:
                return _keyboardKey;
        }
    }

    public string GetLabelForDevice(InputDevice device)
    {
        switch (device)
        {
            case InputDevice.KeyboardMouse:
                return _keyboardLabel;

            case InputDevice.Xbox:
                return _gamepadLabel;

            case InputDevice.PlayStation:
                return _playstationLabel;

            default:
                return _keyboardLabel;
        }
    }

    // Utility


    public static string GetDefaultKeyDisplayName(KeyCode key)
    {
        if (key == KeyCode.None)
            return "Unbound";

        return key.ToString();
    }
}