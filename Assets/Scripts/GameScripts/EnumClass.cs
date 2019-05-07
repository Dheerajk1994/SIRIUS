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
        IRON = 26,
        //MOON
        MOON_DIRT = 40,
        MOON_STONE,
        MOON_SAND,

        //DESERT
        DESERT_DIRT=50,
        DESERT_STONE,
        DESERT_GRASS,
        DESERT_FLOWER,
        DESERT_TREE_TRUNK,
        DESERT_TREE_BASE,
        DESERT_TREE_LEAF,

        //SNOW
        SNOW_DIRT = 60,
        SNOW_STONE,
        SNOW_GRASS,
        SNOW_FLOWER,
        SNOW_TREE_TRUNK,
        SNOW_TREE_BASE,
        SNOW_TREE_LEAF,

        CAMPFIRE = 100,

        BED_ROCK = 500
    };

    public enum TerrainType
    {
        GREEN = 0,
        MOON,
        DESERT,
        SNOW,
        ASTEROID,
        SHIP
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

    public enum EnemyEnum
    {
        BLOB = 0
    };


}
    