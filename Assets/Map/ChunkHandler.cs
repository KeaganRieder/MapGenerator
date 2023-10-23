using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*about
 * handles chunks in the game
 * converts values form global to local
 * to allow accessing contents
 */
public class ChunkHandler 
{    
    private Transform gameWorld;
    private Transform player;
    private Dictionary<Vector2, Chunk> chunks;


    public ChunkHandler(Transform gameWorld)
    {
        chunks = new Dictionary<Vector2, Chunk>();
        this.gameWorld = gameWorld;
        this.player = player;
    }
    public Vector2 GetChunkCords(int globalX, int globalY)
    {
        int chunkXCord = Mathf.RoundToInt(globalX / Chunk.CHUNK_SIZE);
        int chunkYCord = Mathf.RoundToInt(globalY / Chunk.CHUNK_SIZE);
        return new Vector2(chunkXCord, chunkYCord);
    }
    public void CheckIfChunkExsits(int globalX, int globalY)
    {     
        if (chunks.ContainsKey(GetChunkCords(globalX, globalY)))
        {
        }
        else
        {
            chunks.Add(GetChunkCords(globalX, globalY), new Chunk(GetChunkCords(globalX, globalY), gameWorld));
        }
    }
    public void SetTile(int globalX, int globalY, Sprite sprite, Color color)
    {
        //Debug.Log("Placing tile");
        CheckIfChunkExsits(globalX, globalY);
        Tile temp = new();
        temp.CreateTile(globalX, globalY, chunks[GetChunkCords(globalX, globalY)].GetChunkObject().transform,sprite,color);
        chunks[GetChunkCords(globalX, globalY)].SetFloor(globalX, globalY, temp);
    }
    public Tile GetFloor(int globalX, int globalY)
    {
        return chunks[GetChunkCords(globalX, globalY)].GetFloor(globalX, globalY);
    }

    public void UpdateVisableChunks()
    {

    }

   
}


