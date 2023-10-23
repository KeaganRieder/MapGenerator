using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* about class
 * data class holding all info that relates to the games map
 * 
 */
[System.Serializable]
public class MapData 
{
    //map size
    public int mapWidth;
    public int mapHeight;

    //generation info
    public int seed;

    public float avgElevation;
    public float avgMoisture;
    public float avgTempeture;

    public float mountainBase;
    public float waterLevel;

    //spawn info
    //todo

    //other info
    public const float MAX_VIEW_DIST = 64;
    public const int CHUNK_SIZE = 32;
}
