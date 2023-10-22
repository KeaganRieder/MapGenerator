using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    public const int CHUNK_SIZE = 32;
    public MapData data;
    public float lacunarity;
    public int octaves = 10;
    public float noiseScale = 50;
    public Vector2 offset = new();
    public Sprite sprite;

    private GameObject gameWorld;

    public void GenerateMap()
    {
        gameWorld = new GameObject("GameWorld");
        gameWorld.transform.position = new Vector3(0, 0, 0);
        float[,] elevationMap = PerlinNoiseMap.Generate(data.mapWidth, data.mapHeight, data.seed, noiseScale, octaves, data.avgElevation, lacunarity, offset);
        float[,] moistureMap = PerlinNoiseMap.Generate(data.mapWidth, data.mapHeight, data.seed, noiseScale, octaves, data.avgMoisture, lacunarity, offset);
        //terrain
        for (int x = 0; x < data.mapWidth; x++)
        {
            for (int y = 0; y < data.mapHeight; y++)
            {
                if (elevationMap[x,y] < data.waterLevel)
                {
                    elevationMap[x, y] = 0;
                }
                if (elevationMap[x, y] > data.mountainBase)
                {
                    elevationMap[x, y] = 1;
                }
                CreateTile(x, y, elevationMap[x, y], elevationMap[x, y], elevationMap[x, y], .5f);
                
            }
        }
       
    }

    public void CreateTile(int x, int y, float r, float g, float b,float a)
    {
        GameObject tile = new GameObject("tile");
        tile.AddComponent<SpriteRenderer>();
        //tile.AddComponent<TileData<tempClass>>();
        tile.GetComponent<SpriteRenderer>().sprite = sprite;// graphic.ConvertToSprite();
        tile.GetComponent<SpriteRenderer>().color = new Color(r, g, b, a);
      
        tile.transform.SetParent(gameWorld.transform, false);
        tile.transform.position = new Vector3(x, y, 0);
    }

}