using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* about
 * generate the map for the game
 * this includes the preview map, and then final map
 * after settings have been finalized
 * along with generating new chunks
 */
public class MapGenerator
{
    public MapData data;
    public float lacunarity;
    public int octaves = 10;
    public float noiseScale = 50;
    public Vector2 offset = new();
    public Sprite sprite;


    private ChunkHandler chunkHandler;
    private GameObject gameWorld;
    //private Tile[,] gameTiles;
    public void Initalize()
    {
        gameWorld = new GameObject("GameWorld");
        gameWorld.transform.position = new Vector3(0, 0, 0);
        chunkHandler = new ChunkHandler(gameWorld.transform);
    }

    public void GenerateMap()
    {
        float[,] elevationMap = PerlinNoiseMap.Generate(data.mapWidth, data.mapHeight, data.seed, noiseScale, octaves, data.avgElevation, lacunarity, offset);
        //Debug.Log("Generating");
        for (int x = 0; x < data.mapWidth; x++)
        {
            for (int y = 0; y < data.mapHeight; y++)
            {
                Color color = new Color(elevationMap[x, y], elevationMap[x, y], elevationMap[x, y]);
                chunkHandler.SetTile(x, y,sprite,color);
            }
        }
       
    }

}
