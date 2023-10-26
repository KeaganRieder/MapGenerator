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
    private int visableChunks;
    private Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();
    private List<Chunk> chunksLastUpdated = new List<Chunk>();
    private MapGenerator mapGenerator;

    public ChunkHandler(MapGenerator generator)
    {
        visableChunks = Mathf.RoundToInt(MapData.MAX_VIEW_DIST / MapData.CHUNK_SIZE - 1);
        this.mapGenerator = generator;
    }
    public Vector2 GetChunkCords(float globalX, float globalY)
    {
        int chunkXCord = Mathf.RoundToInt(globalX / MapData.CHUNK_SIZE);
        int chunkYCord = Mathf.RoundToInt(globalY / MapData.CHUNK_SIZE);
        return new Vector2(chunkXCord, chunkYCord);
    }

    //modifing parts of the chunk
    public void SetGround(int globalX, int globalY, Tile ground)
    {
       // chunks[GetChunkCords(globalX, globalY)].SetGround(globalX, globalY, ground);
    }
    public void SetGround(Vector2 chunkCords, int x, int y, Tile ground)
    {
      //  chunks[chunkCords].SetGround(x, y, ground);
    }
    public Tile GetGround(int x, int y)
    {
        return chunks[new Vector2(x, y)].GetGround(x, y);
    }
    public void SetFloor(int globalX, int globalY, Tile floor)
    {
        chunks[GetChunkCords(globalX, globalY)].SetFloor(globalX, globalY, floor);
    }
    public Tile GetFloor(int globalX, int globalY)
    {
        return chunks[GetChunkCords(globalX, globalY)].GetFloor(globalX, globalY);
    }

    //updating chunk Visuals and other things handling chunks
    public void UpdateVisableChunks(Vector2 veiwerPostion)
    {
        for (int i = 0; i < chunksLastUpdated.Count; i++)
        {
            chunksLastUpdated[i].SetVisible(false);
        }
        chunksLastUpdated.Clear();

        Vector2 currentChunkCords = GetChunkCords(veiwerPostion.x, veiwerPostion.y);

        for (int xOffset = -visableChunks; xOffset <= visableChunks; xOffset++)
        {
            for (int yOffset = -visableChunks; yOffset <= visableChunks; yOffset++)
            {
                Vector2 veiwedChunkCord = new Vector2(currentChunkCords.x + xOffset, currentChunkCords.y + yOffset);
                if (chunks.ContainsKey(veiwedChunkCord))
                {
                    chunks[veiwedChunkCord].UpdateChunk(veiwerPostion);
                    if (chunks[veiwedChunkCord].IsVisible())
                    {
                        chunksLastUpdated.Add(chunks[veiwedChunkCord]);
                    }
                }
                else
                {
                    chunks.Add(veiwedChunkCord, new Chunk(veiwedChunkCord));
                    chunks[veiwedChunkCord].CreateChunk(veiwedChunkCord, mapGenerator);
                }
            }
        }


    }

}