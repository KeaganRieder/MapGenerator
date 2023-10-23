using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* about ChunkData
 *  defines what a chunk in the game is meant to be
 *  a chunk is meant to hold a collection of tiles
 *  in a CHUNK_SIZE x CHUNK_SIZE space. it also 
 *  holds other data that relates to the chunk
 */
public class Chunk
{
    public const int CHUNK_SIZE = 32;
    private GameObject chunkObject;
    private Dictionary<Vector2,Tile> chunkContents = new Dictionary<Vector2, Tile>();
    private Vector2 postion;
    public Chunk(Vector2 cord, Transform parent)
    {
        chunkObject = new GameObject($"Chunk{cord}");
        postion = cord * CHUNK_SIZE;
        chunkObject.transform.SetParent(parent, false);
        chunkObject.transform.position = postion;
    }
    public GameObject GetChunkObject()
    {
        return chunkObject;
    }
    
    public void SetFloor(int x, int y, Tile data)
    {
        chunkContents.Add(new Vector2(x, y), data);
    }
    public Tile GetFloor(int x, int y)
    {        
        return chunkContents[new Vector2(x,y)];
    }

}
