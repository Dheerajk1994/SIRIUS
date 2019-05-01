using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheImmortalScript : MonoBehaviour {

    private EnumClass.TerrainType worldTypeToGenerate;
    private EnumClass.TerrainType terrainGenerated;
    private bool isNewGame;

    public EnumClass.TerrainType WorldTypeToGenerate{ get { return worldTypeToGenerate; } set { worldTypeToGenerate = value; } }
    public EnumClass.TerrainType TerrainGenerated{ get { return terrainGenerated; } set { terrainGenerated = value; } }
    public bool IsNewGame { get; set; }

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
