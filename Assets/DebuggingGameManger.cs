using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* about
 * the games manger
 * controls various aspects of the game
 * this version is also meant for testing and debuging
 */
public class DebuggingGameManger : MonoBehaviour
{
    public MapData data;
    public int octaves = 10;
    public float noiseScale = 50;
    public float lacunarity;
    public Vector2 offset;
    public bool update = false;
    public bool finalizeGeneration = false;
    public Sprite sprite;

    private MapGenerator mapGenerator = new MapGenerator();
    public ChunkHandler chunkHandler;
    public Transform Player;

    public float heightCurveMultipler; //todo
    public AnimationCurve heightCurve; //todo

    private void Start()
    {
        mapGenerator.data = data;
        mapGenerator.lacunarity = lacunarity;
        mapGenerator.octaves = octaves;
        mapGenerator.noiseScale = noiseScale;
        mapGenerator.offset = offset;
        mapGenerator.sprite = sprite;
        mapGenerator.heightCurveMultipler = heightCurveMultipler;
        mapGenerator.heightCurve = heightCurve;
        mapGenerator.Initalize();
        //chunkHandler = new(mapGenerator);
        chunkHandler= mapGenerator.chunkHandler;
        
       // mapGenerator.GenerateMap();
    }

    private void Update()
    {
        if (update)
        {
            if (GameObject.Find("GameWorld") != null)
            {
                Destroy(GameObject.Find("GameWorld"));
               // Destroy(GameObject.Find("MoistureMap"));
            }           
           
            mapGenerator.data = data;
            mapGenerator.lacunarity = lacunarity;
            mapGenerator.octaves = octaves;
            mapGenerator.noiseScale = noiseScale;
            mapGenerator.offset = offset;
            mapGenerator.sprite = sprite;
            mapGenerator.Initalize();
           // mapGenerator.GenerateMap();
           // update = false;


        }
        Vector2 veiwerPosition = new Vector2(Player.position.x , Player.position.y);
        chunkHandler.UpdateVisableChunks(veiwerPosition);
    }
}
