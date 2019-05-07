using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleInputScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        // Where all actions are managed
        if(InputManager.instance.KeyDown("Left")){
            //Do action
            Debug.Log("Left Key Pressed");
        }
        if (InputManager.instance.KeyDown("Right"))
        {
            Debug.Log("Right Key Pressed");
        }
        if (InputManager.instance.KeyDown("Jump"))
        {
            Debug.Log("Jump Key Pressed");
        }
        if (InputManager.instance.KeyDown("Hotkey1"))
        {
            Debug.Log("Hotkey1 Key Pressed");
        }
        if (InputManager.instance.KeyDown("Hotkey2"))
        {
            Debug.Log("Hotkey2 Key  Pressed");
        }
        if (InputManager.instance.KeyDown("Hotkey3"))
        {
            Debug.Log("Hotkey3 Key Pressed");
        }
        if (InputManager.instance.KeyDown("Hotkey4"))
        {
            Debug.Log("Hotkey4 Key Pressed");
        }
        if (InputManager.instance.KeyDown("Hotkey5"))
        {
            Debug.Log("Hotkey5 Key Pressed");
        }
        if (InputManager.instance.KeyDown("Hotkey6"))
        {
            Debug.Log("Hotkey6 Key Pressed");
        }
        if (InputManager.instance.KeyDown("Hotkey7"))
        {
            Debug.Log("Hotkey7 Key Pressed");
        }
        if (InputManager.instance.KeyDown("Hotkey8"))
        {
            Debug.Log("Hotkey8 Key Pressed");
        }
        if (InputManager.instance.KeyDown("Hotkey9"))
        {
            Debug.Log("Hotkey9 Key Pressed");
        }
        if (InputManager.instance.KeyDown("Hotkey0"))
        {
            Debug.Log("Hotkey0 Key Pressed");
        }
        if (InputManager.instance.KeyDown("PlayerAction"))
        {
            Debug.Log("PlayerAction Key Pressed");
        }
        if (InputManager.instance.KeyDown("PlayerAltAction"))
        {
            Debug.Log("PlayerAltAction Key Pressed");
        }
        if (InputManager.instance.KeyDown("Pause"))
        {
            Debug.Log("Pause Key Pressed");
        }
    }
}
