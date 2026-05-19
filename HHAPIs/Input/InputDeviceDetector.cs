using System;
using UnityEngine;

public class InputDeviceDetector : MonoBehaviour
{
    private const bool TEMP_DISABLE_GAMEPAD = true;

    private static InputDevice _activeDevice;
    private static InputDevice? _forcedDevice;

    private const float STICK_SWITCH_THRESHOLD = 0.8f;

    public static InputDevice ActiveDevice => _activeDevice;

    public static event Action<InputDevice> OnActiveDeviceChanged;

    private void Awake()
    {
        _activeDevice = DetectDevice();
    }

    public static void SetForcedDevice(InputDevice? device)
    {
        _forcedDevice = device;

        if (device.HasValue)
        {
            SetActiveDevice(device.Value);
        }
    }

    private void Update()
    {
        if (_forcedDevice.HasValue)
            return;

        InputDevice detected = DetectDevice();

        if (detected != _activeDevice)
        {
            SetActiveDevice(detected);
        }
    }

    private static void SetActiveDevice(InputDevice device)
    {
        _activeDevice = device;
        OnActiveDeviceChanged?.Invoke(_activeDevice);
    }

    private InputDevice DetectDevice()
    {
        if (TEMP_DISABLE_GAMEPAD)
            return InputDevice.KeyboardMouse;

        // Mouse always wins if used
        if (AnyMouseActivity())
            return InputDevice.KeyboardMouse;

        // Gamepad wins if used
        if (AnyGamepadButtonInput() || AnyGamepadStickInput())
            return InputDevice.Xbox;

        return _activeDevice;
    }

    private bool AnyMouseActivity()
    {
        return AnyMouseMoved() || AnyMouseClicked();
    }

    private bool AnyMouseClicked()
    {
        return Input.GetMouseButtonDown(0) ||
               Input.GetMouseButtonDown(1) ||
               Input.GetMouseButtonDown(2);
    }

    private bool AnyMouseMoved()
    {
        return Mathf.Abs(Input.GetAxis("Mouse X")) > 0.01f ||
               Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f;
    }

    private bool AnyGamepadButtonInput()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton0) ||
               Input.GetKeyDown(KeyCode.JoystickButton1) ||
               Input.GetKeyDown(KeyCode.JoystickButton2) ||
               Input.GetKeyDown(KeyCode.JoystickButton3);
    }

    private bool AnyGamepadStickInput()
    {
        return Mathf.Abs(Input.GetAxis("Horizontal")) > STICK_SWITCH_THRESHOLD ||
               Mathf.Abs(Input.GetAxis("Vertical")) > STICK_SWITCH_THRESHOLD;
    }
}