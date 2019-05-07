using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    


    public static InputManager instance;
    private Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        keybinds.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keybinds.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keybinds.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        keybinds.Add("Hotkey1", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Hotkey1", "Alpha1")));
        keybinds.Add("Hotkey2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Hotkey2", "Alpha2")));
        keybinds.Add("Hotkey3", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Hotkey3", "Alpha3")));
        keybinds.Add("Hotkey4", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Hotkey4", "Alpha4")));
        keybinds.Add("Hotkey5", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Hotkey5", "Alpha5")));
        keybinds.Add("Hotkey6", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Hotkey6", "Alpha6")));
        keybinds.Add("Hotkey7", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Hotkey7", "Alpha7")));
        keybinds.Add("Hotkey8", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Hotkey8", "Alpha8")));
        keybinds.Add("Hotkey9", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Hotkey9", "Alpha9")));
        keybinds.Add("Hotkey0", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Hotkey0", "Alpha0")));
        keybinds.Add("PlayerAction", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerAction", "Mouse0")));
        keybinds.Add("PlayerAltAction", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerAltAction", "Mouse1")));
        keybinds.Add("Pause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pause", "Escape")));
    }

    public bool KeyDown(string key)
    {
        if (Input.GetKeyDown(keybinds[key]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setKey(string name, KeyCode key)
    {
        keybinds[name] = key;
    }

    public KeyCode getKey(string key)
    {
        return keybinds[key];
    }

    public void SaveKeys()
    {
        foreach(var key in keybinds)
        {
            PlayerPrefs.SetString(key.Key, keybinds.Values.ToString());
            Debug.Log(key.Key + " added to preference with key " + keybinds.Values.ToString());
        }
        Debug.Log("Saved");
        PlayerPrefs.Save();
    }
}
