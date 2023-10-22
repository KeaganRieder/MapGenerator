using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public MapData data;
    public int octaves = 10;
    public float noiseScale = 50;
    public float lacunarity;
    public Vector2 offset;
    public bool autoUpdate = false;
    public bool finalizeGeneration = false;
    public Sprite sprite;

    private MapGenerator mapGenerator = new MapGenerator();


    private void Start()
    {
        mapGenerator.data = data;
        mapGenerator.lacunarity = lacunarity;
        mapGenerator.octaves = octaves;
        mapGenerator.noiseScale = noiseScale;
        mapGenerator.offset = offset;
        mapGenerator.sprite = sprite;       
        mapGenerator.GenerateMap();
    }

    private void Update()
    {
        if (autoUpdate)
        {
            if (GameObject.Find("GameWorld") != null)
            {
                Destroy(GameObject.Find("GameWorld"));
                Destroy(GameObject.Find("MoistureMap"));
            }           
           
            mapGenerator.data = data;
            mapGenerator.lacunarity = lacunarity;
            mapGenerator.octaves = octaves;
            mapGenerator.noiseScale = noiseScale;
            mapGenerator.offset = offset;
            mapGenerator.sprite = sprite;
            mapGenerator.GenerateMap();
            //autoUpdate = false;


        }
    }
}
