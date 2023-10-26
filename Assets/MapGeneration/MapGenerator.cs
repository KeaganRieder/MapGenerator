using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* about 
 * GeneratedMapData is a temporary data class holding the 
 * resulting data created from map generator, and used
 * to pass that data in a organized way in order for it to
 * be used in finalizing generation of a chunk or
 * map preview
 */
public class GeneratedChunk //maybe rename to generatedMapData?
{
    //nosie maps

    public Dictionary<Vector2, Tile> chunkGround = new Dictionary<Vector2, Tile>();
    public Dictionary<Vector2, Tile> chunkFloor = new Dictionary<Vector2, Tile>();
    public Dictionary<Vector2, Tile> chunkBiulding = new Dictionary<Vector2, Tile>();
}

/* about
 * generate the map for the game
 * this includes the preview map, and then final map
 * after settings have been finalized
 * along with generating new chunks
 */
public class MapGenerator
{
    public Sprite sprite;//this is temporary
    public ChunkHandler chunkHandler;
    public GameObject gameWorld;
    public MapData data;
    public float lacunarity;
    public int octaves = 10;
    public float noiseScale = 50;
    public Vector2 offset = new();

    public float heightCurveMultipler; //todo
    public AnimationCurve heightCurve; //todo
    //private float[,] elevationMap;

    //make this in the constructor
    public void Initalize()
    {
        gameWorld = new GameObject("GameWorld");
        gameWorld.transform.position = new Vector3(0, 0, 0);
        chunkHandler = new ChunkHandler(this);
    }

    public void GeneratePreviewMap(Vector2 position)//make this for editor/act as a preview
    {
        //todo
    }

    public GeneratedChunk GenerateChunk(Vector2 position, Transform chunk)
    {
        GeneratedChunk generatedChunk = new GeneratedChunk();
        PerlinNoiseMap perlinNoiseMap = new(MapData.CHUNK_SIZE, MapData.CHUNK_SIZE, data.seed, noiseScale, octaves, data.avgElevation, lacunarity, position+ offset, NoiseMap.NormalizationMode.Global);
        perlinNoiseMap.ApplyCurve(heightCurve, heightCurveMultipler);
        float[,] elevationMap = perlinNoiseMap.GetNoiseMap();

        for (int x = 0; x < MapData.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < MapData.CHUNK_SIZE; y++)
            {
                float noiseValue = elevationMap[x, y];

                Color color = new Color(noiseValue, noiseValue, noiseValue);//get rid of

                int tileXCords = x + ((int)position.x);
                int tileYCords = y + ((int)position.y);
                Tile generatedTile = new Tile(x, y, chunk, sprite, color);

                generatedChunk.chunkGround.Add(new Vector2(tileXCords, tileYCords),generatedTile);

            }
        }
        return generatedChunk;

    }
   
}
