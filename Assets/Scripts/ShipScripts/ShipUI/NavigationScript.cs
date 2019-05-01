using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationScript : MonoBehaviour {

    private AudioManagerScript audiomanager;
    private Animator navPanelAnimator;
    private bool isOpen = false;

    public int requiredFuelForTravel;

    [SerializeField] private Button exitButn;
    [SerializeField] private Button setSailButton;

    [SerializeField] private Text planetName;
    [SerializeField] private Text planetInfo;
    [SerializeField] private Text fuelInfo;
    [SerializeField] private Image currentPlanetIcon;

    [SerializeField] private Button GreenPlanet;
    [SerializeField] private Button MoonPlanet;
    [SerializeField] private Button SnowPlanet;
    [SerializeField] private Button DesertPlanet;

    [SerializeField] private Transform greenIconPos;
    [SerializeField] private Transform moonIconPos;
    [SerializeField] private Transform snowIconPos;
    [SerializeField] private Transform desertIconPos;

    public EnumClass.TerrainType currentPlanet;
    public EnumClass.TerrainType planetToMoveTo;

    public Engine engineObjectScript;

    UIScript uiScript;

    public void SetNavPanel(UIScript uScript)
    {
        GreenPlanet.onClick.AddListener(GreenPlanetClicked);
        MoonPlanet.onClick.AddListener(MoonPlanetClicked);
        SnowPlanet.onClick.AddListener(IcePlanetClicked);
        DesertPlanet.onClick.AddListener(DesertPlanetClicked);

        setSailButton.onClick.AddListener(SetSailClicked);
        exitButn.onClick.AddListener(ClosePanelClicked);

        uiScript = uScript;
        audiomanager = uScript.audioManager.GetComponent<AudioManagerScript>();
        engineObjectScript = uiScript.gameManagerScript.ship.GetComponent<ShipScript>().GetEngineReference().GetComponent<Engine>();
    }

    private void Start()
    {
        navPanelAnimator = this.GetComponent<Animator>();
        //enginePanelToggleButn.onClick.AddListener(ToggleEnginePanel);
    }

    public void ToggleNavPanel(bool toggle)
    {
        Debug.Log("toggle 1 called");
        isOpen = toggle;
        navPanelAnimator.SetBool("isOpen", toggle);

        audiomanager.Play("ui-animation");

        if (isOpen)
        {
            currentPlanet = TheImmortalScript.instance.TerrainGenerated;
            UpdateMarkerPosition();
        }
    }

    public void ToggleNavPanel()
    {
        Debug.Log("toggle 2 called");
        isOpen = !isOpen;
        navPanelAnimator.SetBool("isOpen", isOpen);

        audiomanager.Play("ui-animation");


        if(isOpen)
        {
            currentPlanet = TheImmortalScript.instance.TerrainGenerated;
            UpdateMarkerPosition();
        }
    }

    public void ClosePanelClicked(){
        ToggleNavPanel(false);
        audiomanager.Play("btn-confirm");
    }

    public void GreenPlanetClicked() { 
        audiomanager.Play("btn-quick-ui");
        planetName.text = "Green World";
        planetInfo.text = "A lush green land with plenty of trees and vegetation.\n" +
            "Common resources: normal\n" +
            "Rare resources: low\n"+
            "Vegetation: high\n" +
            "Temperature: mild\n"+
            "Threat level: normal";
        planetToMoveTo = EnumClass.TerrainType.GREEN;
        UpdateSetSailButton();
    }

    public void MoonPlanetClicked(){
        audiomanager.Play("btn-quick-ui");
        planetName.text = "Moon World";
        planetInfo.text = "A barren lifeless rock.\n" +
            "Common resources: low\n" +
            "Rare resources: high\n" +
            "Vegetation: none\n" +
            "Temperature: low\n" +
            "Threat level: low";
        planetToMoveTo = EnumClass.TerrainType.MOON;
        UpdateSetSailButton();
    }

    public void IcePlanetClicked(){
        audiomanager.Play("btn-quick-ui");
        planetName.text = "Ice World";
        planetInfo.text = "A harsh icey planet. Bring a jacket\n" +
            "Common resources: normal\n" +
            "Rare resources: normal\n" +
            "Vegetation: low\n" +
            "Temperature: low\n" +
            "Threat level: normal";
        planetToMoveTo = EnumClass.TerrainType.SNOW;
        UpdateSetSailButton();
    }

    public void DesertPlanetClicked(){
        audiomanager.Play("btn-quick-ui");
        planetName.text = "The Deserts of Sivanium";
        planetInfo.text = "A hot and dry planet.\n" +
            "Common resources: normal\n" +
            "Rare resources: high\n" +
            "Vegetation: low\n" +
            "Temperature: high\n" +
            "Threat level: normal";
        planetToMoveTo = EnumClass.TerrainType.DESERT;
        UpdateSetSailButton();
    }

    private void UpdateSetSailButton()
    {
        fuelInfo.text = "Fuel: " + engineObjectScript.currentFuel.ToString() + "/" +  requiredFuelForTravel.ToString();
        if (!SeeIfEnoughFuel())
        {
            setSailButton.interactable = false;
            fuelInfo.color = Color.red;
        }
        else
        {
            setSailButton.interactable = true;
            fuelInfo.color = Color.white;
        }
    }

    private bool SeeIfEnoughFuel()
    {
        return (engineObjectScript.currentFuel >= requiredFuelForTravel);
    }

    private void UpdateMarkerPosition()
    {
        switch (currentPlanet)
        {
            case EnumClass.TerrainType.GREEN:
                currentPlanetIcon.transform.position = greenIconPos.position;
                GreenPlanetClicked();
                break;
            case EnumClass.TerrainType.MOON:
                currentPlanetIcon.transform.position = moonIconPos.position;
                MoonPlanetClicked();
                break;
            case EnumClass.TerrainType.DESERT:
                currentPlanetIcon.transform.position = desertIconPos.position;
                DesertPlanetClicked();
                break;
            case EnumClass.TerrainType.SNOW:
                currentPlanetIcon.transform.position = snowIconPos.position;
                IcePlanetClicked();
                break;
            case EnumClass.TerrainType.ASTEROID:
                //currentPlanetIcon.transform.position = greenIconPos;
                break;
            case EnumClass.TerrainType.SHIP:
                break;
            default:
                break;
        }
    }

    public void SetSailClicked() {
        //play some sound
        audiomanager.Play("btn-confirm");
        //shake the ship?

        //take out fuel
        engineObjectScript.Travel(requiredFuelForTravel);

        //hange current planet
        currentPlanet = planetToMoveTo;
        TheImmortalScript.instance.TerrainGenerated = currentPlanet;
        UpdateMarkerPosition();
        UpdateSetSailButton();

        //set the background
    }
}
