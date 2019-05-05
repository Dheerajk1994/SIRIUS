using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour {

    [SerializeField] private GameObject Teleporter;
    [SerializeField] private GameObject Engine;
    [SerializeField] private GameObject Navigation;
    [SerializeField] private GameObject Bed;
    [SerializeField] private GameObject Chest;

    public AudioManagerScript audioManager;
    private GameManagerScript gameManager;


    public Transform spawnPosition;

    public void SetShip(GameManagerScript gm, UIScript uiscript)
    {
        gameManager = gm;
        Teleporter.GetComponent<Teleporter>().ExitShipPanel = uiscript.ExitShipPanel;
        Engine.GetComponent<Engine>().EnginePanel = uiscript.EnginePanel;
        Navigation.GetComponent<Navigation>().NavigationPanel = uiscript.NavPanel;
        Chest.GetComponent<Chest>().SetChest(uiscript);
    }

    public GameObject GetEngineReference()
    {
        return Engine;
    }

    public GameObject GetNavigationReference()
    {
        return Navigation;
    }

    public GameObject GetChestReference()
    {
        return Chest;
    }

}
