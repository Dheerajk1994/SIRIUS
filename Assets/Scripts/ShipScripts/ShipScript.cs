using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour {

    [SerializeField] private GameObject Teleporter;
    [SerializeField] private GameObject Engine;
    [SerializeField] private GameObject Navigation;
    [SerializeField] private GameObject Bed;
    [SerializeField] private GameObject Chest;
    [SerializeField] private GameObject DownStairs;
    [SerializeField] private GameObject UpStairs;


    public AudioManagerScript audioManager;

    public Transform spawnPosition;

    public void SetShip(UIScript uiscript, AudioManagerScript aManager)
    {
        Teleporter.GetComponent<Teleporter>().ExitShipPanel = uiscript.ExitShipPanel;
        Engine.GetComponent<Engine>().EnginePanel = uiscript.EnginePanel;
        Navigation.GetComponent<Navigation>().NavigationPanel = uiscript.NavPanel;
        Chest.GetComponent<Chest>().ChestPanel = uiscript.ChestPanel;
        audioManager = aManager;
        DownStairs.GetComponent<Door>().SetDoor(audioManager);
        UpStairs.GetComponent<Door>().SetDoor(audioManager);

    }

    public void Start()
    {
        audioManager.Play("bgm-1");
    }

    public GameObject GetEngineReference()
    {
        return Engine;
    }

    public GameObject GetNavigationReference()
    {
        return Navigation;
    }

}
