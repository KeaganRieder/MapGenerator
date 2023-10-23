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
    private Chunk[,] chunks;

    public ChunkHandler(MapData data)
    {
        int xChunks = data.mapWidth / Chunk.CHUNK_SIZE;
        int yChunks = data.mapHeight / Chunk.CHUNK_SIZE;
        chunks = new Chunk[xChunks, yChunks];
    }

    public void CheckForChunk(int globalX, int globalY)
    {

    }

    public void SetFloor(int globalX, int globalY, tempClass data)
    {
        int chunkX = Mathf.FloorToInt((float)globalX / Chunk.CHUNK_SIZE);
        int chunkY = Mathf.FloorToInt((float)globalY / Chunk.CHUNK_SIZE);

        chunks[chunkX, chunkY].SetFloor(globalX - chunkX * 16, globalY - chunkY * 16, data);
    }
}
