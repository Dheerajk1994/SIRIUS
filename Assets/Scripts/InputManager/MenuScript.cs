using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    Transform menuPanel;
    Event keyEvent;

    private Color32 unselected = new Color32(39, 171, 249, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);

    Text buttonText;
    GameObject currentKey;

    bool waitingForKeyPress;

    // Use this for initialization
    void Start()
    {
        menuPanel = transform.Find("Panel");

        for(int i = 0; i < menuPanel.childCount; ++i)
        {
            
            if (menuPanel.GetChild(i).name == "Left")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.instance.getKey("Left").ToString();
            else if (menuPanel.GetChild(i).name == "Right")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.instance.getKey("Right").ToString();
            else if (menuPanel.GetChild(i).name == "Jump")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.instance.getKey("Jump").ToString();
            else if (menuPanel.GetChild(i).name == "PlayerAction")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.instance.getKey("PlayerAction").ToString();
            else if (menuPanel.GetChild(i).name == "PlayerAltAction")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.instance.getKey("PlayerAltAction").ToString();

            
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnGUI()
    {
        if(currentKey != null)
        {
            keyEvent = Event.current;
            if(keyEvent.isKey)
            {
                InputManager.instance.setKey(currentKey.name, keyEvent.keyCode);
                currentKey.GetComponentInChildren<Text>().text = keyEvent.keyCode.ToString();
                currentKey.GetComponent<Image>().color = unselected;
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if(currentKey != null)
        {
            currentKey.GetComponent<Image>().color = unselected;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

}