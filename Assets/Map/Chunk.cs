using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* about Chunk
 *  defines what a chunk in the game is meant to be
 *  a chunk is meant to hold a collection of tiles
 *  in a CHUNK_SIZE x CHUNK_SIZE space. it also 
 *  holds other data that relates to the chunk
 */
public class Chunk
{
    //private ChunkData data;
    //info for chunk
    public GameObject chunkObject;
    public Vector2 position;
    public Bounds bounds;

    //chunk content varibles 
    public Dictionary<Vector2, Tile> chunkGround = new Dictionary<Vector2, Tile>();
    public Dictionary<Vector2, Tile> chunkFloor = new Dictionary<Vector2, Tile>();
    public Dictionary<Vector2, Tile> chunkBiulding = new Dictionary<Vector2, Tile>();

    //Chunk Creation
    public Chunk(Vector2 cord)
    {
        position = cord * MapData.CHUNK_SIZE;
        Transform parent = GameObject.Find("GameWorld").transform;
        chunkObject = new GameObject($"Chunk{cord}");
        chunkObject.transform.SetParent(parent,false);
        
        bounds = new Bounds(position, Vector2.one * MapData.CHUNK_SIZE);

        SetVisible(true);
    }

    
    public void CreateChunk(Vector2 cord, MapGenerator generator)
    {
        //make it check what point in game this is 
        //and weather it should check if a chunks generated/
        //saved in file and needs to just be loaded

        //chunks
        GeneratedChunk generatedChunk = generator.GenerateChunk(cord, GetChunkTransform());
        chunkGround = generatedChunk.chunkGround;
        chunkObject.transform.position = position;

    }
    
    public GameObject GetChunkObject()
    {
        return chunkObject;
    }

    public Transform GetChunkTransform()
    {
        return chunkObject.transform;
    }

    //stuff to update things in the chunk
    public void SetGround(int x, int y, Tile groundTile)
    {
        //chunkGround.Add(new Vector2(x, y), groundTile);
    }
    public Tile GetGround(int x, int y)
    {
        return chunkGround[new Vector2(x, y)];
    }
    public void SetFloor(int x, int y, Tile floor)
    {
        //todo set ground tile as inavtive if placed if remove 
        //set to active
        chunkFloor.Add(new Vector2(x, y), floor);
    }
    public Tile GetFloor(int x, int y)
    {        
        return chunkFloor[new Vector2(x,y)];
    }


    //Chunk Rendering
    //need to look into this
    public void UpdateChunk(Vector2 veiwerPostion)
    {
        float veiwerDistance = Mathf.Sqrt(bounds.SqrDistance(veiwerPostion));
        bool visible = veiwerDistance <= MapData.MAX_VIEW_DIST;
        SetVisible(visible);
    }
    public bool IsVisible()
    {
        return chunkObject.activeSelf;
    }
    public void SetVisible(bool visible)
    {
        chunkObject.SetActive(visible);
    }

}
