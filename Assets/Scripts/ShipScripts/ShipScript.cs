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
    private GameManagerScript gameManager;


    public Transform spawnPosition;

//<<<<<<< ryan
    public void SetShip(UIScript uiscript, AudioManagerScript aManager)
//=======
   // public void SetShip(GameManagerScript gm, UIScript uiscript)
//>>>>>>> dtemp
    {
        //gameManager = gm;
        Teleporter.GetComponent<Teleporter>().ExitShipPanel = uiscript.ExitShipPanel;
        Engine.GetComponent<Engine>().EnginePanel = uiscript.EnginePanel;
        Navigation.GetComponent<Navigation>().NavigationPanel = uiscript.NavPanel;
//<<<<<<< ryan
        Chest.GetComponent<Chest>().ChestPanel = uiscript.ChestPanel;
        audioManager = aManager;
        DownStairs.GetComponent<Door>().SetDoor(audioManager);
        UpStairs.GetComponent<Door>().SetDoor(audioManager);

   // }

   // public void Start()
   // {
        audioManager.Play("bgm-1");
//=======
        Chest.GetComponent<Chest>().SetChest(uiscript);
//>>>>>>> dtemp
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
