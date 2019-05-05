using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheImmortalScript : MonoBehaviour {

    public enum TerrainStatus { NOT_GENERATED, GENERATED};

    private EnumClass.TerrainType worldTypeToGenerate;
    private EnumClass.TerrainType terrainGenerated;
    private bool isNewGame;

    private ushort [,] playerInventoryItems;
    private ushort [,] playerHotbarItems;

    private ushort [,] shipInventoryItems;
    private int shipFuelAmount;

    private List<int> questsCompleted;
    private List<int> activeQuests;
    private List<int> dialoguesCompleted;

    private TerrainStatus greenWorldStatus;
    private TerrainStatus moonWorldStatus;
    private TerrainStatus snowWorldStatus;
    private TerrainStatus desertWorldStatus;

    private Vector2 playerLandPosInGreen;
    private Vector2 playerLandPosInMoon;
    private Vector2 playerLandPosInSnow;
    private Vector2 playerLandPosInDesert;

    private readonly string greenWorldSavePath  = "greenWorld";
    private readonly string moonWorldSavePath   = "moonWorld";
    private readonly string snowWorldSavePath   = "snowWorld";
    private readonly string desertWorldSavePath = "desertWorld";


    public EnumClass.TerrainType WorldTypeToGenerate    { get { return worldTypeToGenerate; } set { worldTypeToGenerate = value; } }
    public EnumClass.TerrainType TerrainGenerated       { get { return terrainGenerated; } set { terrainGenerated = value; } }
    public bool IsNewGame                               { get; set; }

    public ushort[,] PlayerInventoryItems               { get { return playerInventoryItems;} set { playerInventoryItems = value; } }
    public ushort[,] PlayerHotbarItems                  { get { return playerHotbarItems;} set { playerHotbarItems = value; } }

    public ushort[,] ShipInventoryItems                 { get { return shipInventoryItems;} set { shipInventoryItems = value; } }
    public int ShipFuelAmount                           { get { return shipFuelAmount; } set { shipFuelAmount = value; } }

    public List<int> QuestsCompleted                    { get { return questsCompleted; } set { questsCompleted = value; } }
    public List<int> ActiveQuests                       { get { return activeQuests; } set { activeQuests = value; } }
    public List<int> DialoguesCompleted                 { get { return dialoguesCompleted; } set { dialoguesCompleted = value; } }

    public TerrainStatus GreenWorldStatus               { get { return greenWorldStatus; } set { greenWorldStatus = value; } }
    public TerrainStatus MoonWorldStatus                { get { return moonWorldStatus; } set { moonWorldStatus = value; } }
    public TerrainStatus SnowWorldStatus                { get { return snowWorldStatus; } set { snowWorldStatus = value; } }
    public TerrainStatus DesertWorldStatus              { get { return desertWorldStatus; } set { desertWorldStatus = value; } }

    public Vector2 PlayerLandPosInGreen                 { get { return playerLandPosInGreen; } set { playerLandPosInGreen = value; } }
    public Vector2 PlayerLandPosInMoon                  { get { return playerLandPosInMoon; } set { playerLandPosInMoon = value; } }
    public Vector2 PlayerLandPosInSnow                  { get { return playerLandPosInSnow; } set { playerLandPosInSnow = value; } }
    public Vector2 PlayerLandPosInDesert                { get { return playerLandPosInDesert; } set { playerLandPosInDesert = value; } }

    public string GreenWorldSavePath                    { get { return greenWorldSavePath; } }
    public string MoonWorldSavePath                     { get { return moonWorldSavePath; } }
    public string SnowWorldSavePath                     { get { return snowWorldSavePath; } }
    public string DesertWorldSavePath                   { get { return desertWorldSavePath; } }


    public static TheImmortalScript instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }
}
