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
    private GameObject chunkObject;
    private Dictionary<Vector2, Tile> chunkContents = new Dictionary<Vector2, Tile>();
    private Vector2 postion;
    private Bounds bounds;

    //Chunk Creation
    public Chunk(Vector2 cord, Transform parent, Sprite sprite)
    {
        chunkObject = new GameObject($"Chunk{cord}");
        postion = cord * MapData.CHUNK_SIZE;
        chunkObject.transform.SetParent(parent, false);
        chunkObject.transform.position = postion;
        bounds = new Bounds(postion, Vector2.one * MapData.CHUNK_SIZE);
        chunkObject.AddComponent<SpriteRenderer>();
        chunkObject.GetComponent<SpriteRenderer>().sprite = sprite;

        SetVisible(false);
    }
    //public vo
    public GameObject GetChunkObject()
    {
        return chunkObject;
    }
    

    //stuff to update things in the chunk
    public void SetFloor(int x, int y, Tile data)
    {
        chunkContents.Add(new Vector2(x, y), data);
    }
    public Tile GetFloor(int x, int y)
    {        
        return chunkContents[new Vector2(x,y)];
    }

    //Chunk Rendering
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
