//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

public static class EnumClass {
    public enum TileEnum
    {
        EMPTY = 0,
        REGULAR_DIRT = 1,
        REGULAR_STONE,
        REGULAR_WOOD,
        REGULAR_SAND,

        REGULAR_TREETRUNK = 17,
        REGULAR_TREELEAF,
        REGULAR_FLOWER,
        REGULAR_GRASS,
        //RESOURCES
        COAL = 21,
        COPPER,
        SILVER,
        GOLD,
        DIAMOND,
        //MOON
        MOON_DIRT = 40,
        MOON_STONE,
        MOON_SAND,

        CAMPFIRE = 100
    };

    public enum TerrainType
    {
        GREEN = 0,
        MOON,
        DESERT,
        SNOW,
        ASTEROID
    };

    public enum LayerIDEnum
    {
        STARSLAYER = 9,
        SKYSHADERLAYER,
        PLANETLAYER,
        CLOUDLAYER,
        TERRAINBACKGROUNDLAYER,
        BACKLAYER = 14,
        BACKLAYER_RESOURCES,
        FRONTLAYER,
        FRONTLAYER_RESOURCES,
        GRASS = 20
    };


}
    