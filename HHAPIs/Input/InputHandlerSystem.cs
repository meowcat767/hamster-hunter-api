using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputHandlerSystem : MonoBehaviour
{
    [Serializable]
    public class Keybind
    {
        public string name;
        public KeyCode key;
        public TMP_Text displayText;

        public Keybind(string name, KeyCode key)
        {
            this.name = name;
            this.key = key;
        }
    }

    private static InputHandlerSystem _instance;

    [SerializeField]
    private List<Keybind> keybinds = new();

    [SerializeField]
    private InputActionRegistry _actionRegistry;

    private Dictionary<string, KeyCode> keybindMap = new();

    private readonly Dictionary<string, bool> _toggleStates = new();

    public static InputHandlerSystem Instance => _instance;

    public static event Action<InputDevice> OnActiveDeviceChanged;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        BuildKeybindMap();

        InputDeviceDetector.OnActiveDeviceChanged += HandleDeviceChanged;
    }

    private void OnDestroy()
    {
        InputDeviceDetector.OnActiveDeviceChanged -= HandleDeviceChanged;
    }

    private void Update()
    {
        UpdateToggleStates();
    }

    private void HandleDeviceChanged(InputDevice newDevice)
    {
        OnActiveDeviceChanged?.Invoke(newDevice);
        UpdateDisplays();
    }

    private void BuildKeybindMap()
    {
        keybindMap.Clear();

        foreach (var keybind in keybinds)
        {
            if (!keybindMap.ContainsKey(keybind.name))
            {
                keybindMap.Add(keybind.name, keybind.key);
            }
        }
    }

    public bool HasKeybind(string keybindName)
    {
        return keybindMap.ContainsKey(keybindName);
    }

    public KeyCode GetKeyCode(string keybindName)
    {
        if (keybindMap.TryGetValue(keybindName, out var key))
            return key;

        return KeyCode.None;
    }

    public bool GetKey(string keybindName)
    {
        return Input.GetKey(GetKeyCode(keybindName));
    }

    public bool GetKeyDown(string keybindName)
    {
        return Input.GetKeyDown(GetKeyCode(keybindName));
    }

    public bool GetKeyUp(string keybindName)
    {
        return Input.GetKeyUp(GetKeyCode(keybindName));
    }

    public bool IsPressed(string keybindName)
    {
        return GetKeyDown(keybindName);
    }

    public bool IsPressing(string keybindName)
    {
        return GetKey(keybindName);
    }

    public bool IsReleased(string keybindName)
    {
        return GetKeyUp(keybindName);
    }

    public void AddKeybind(string name, KeyCode key)
    {
        if (HasKeybind(name))
            return;

        keybinds.Add(new Keybind(name, key));
        keybindMap.Add(name, key);
    }

    public bool ChangeKeybind(string name, KeyCode newKey)
    {
        for (int i = 0; i < keybinds.Count; i++)
        {
            if (keybinds[i].name == name)
            {
                keybinds[i].key = newKey;
                keybindMap[name] = newKey;

                UpdateDisplays();
                return true;
            }
        }

        return false;
    }

    public void RemoveKeybind(string name)
    {
        keybinds.RemoveAll(k => k.name == name);

        if (keybindMap.ContainsKey(name))
            keybindMap.Remove(name);
    }

    private void UpdateToggleStates()
    {
        foreach (var keybind in keybinds)
        {
            if (GetKeyDown(keybind.name))
            {
                if (_toggleStates.ContainsKey(keybind.name))
                {
                    _toggleStates[keybind.name] =
                        !_toggleStates[keybind.name];
                }
                else
                {
                    _toggleStates[keybind.name] = true;
                }
            }
        }
    }

    public bool GetToggleState(string keybindName)
    {
        if (_toggleStates.TryGetValue(keybindName, out bool value))
            return value;

        return false;
    }

    private void UpdateDisplays()
    {
        foreach (var keybind in keybinds)
        {
            if (keybind.displayText != null)
            {
                keybind.displayText.text =
                    GetKeyDisplayName(keybind.key);
            }
        }
    }

    public string GetActionLabel(string keybindName)
    {
        return GetKeyDisplayName(GetKeyCode(keybindName));
    }

    private string GetKeyDisplayName(KeyCode keyCode)
    {
        return keyCode.ToString();
    }
}