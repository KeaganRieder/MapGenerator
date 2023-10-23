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
    private Transform gameWorld;
    private Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();
    private List<Chunk> chunksLastUpdated = new List<Chunk>();

    //temporary need to figure out better way to generate chunks
    public Sprite sprite;

    public ChunkHandler(Transform gameWorld)
    {
        this.gameWorld = gameWorld;
        visableChunks = Mathf.RoundToInt(MapData.MAX_VIEW_DIST / MapData.CHUNK_SIZE);
    }
    public Vector2 GetChunkCords(float globalX, float globalY)
    {
        int chunkXCord = Mathf.RoundToInt(globalX / MapData.CHUNK_SIZE);
        int chunkYCord = Mathf.RoundToInt(globalY / MapData.CHUNK_SIZE);
        return new Vector2(chunkXCord, chunkYCord);
    }

    //modifing parts of the chunk
    public void SetTile(int globalX, int globalY, Sprite sprite, Color color)
    {
        Tile temp = new();
        temp.CreateTile(globalX, globalY, chunks[GetChunkCords(globalX, globalY)].GetChunkObject().transform,sprite,color);
        chunks[GetChunkCords(globalX, globalY)].SetFloor(globalX, globalY, temp);
    }
    public Tile GetFloor(int globalX, int globalY)
    {
        return chunks[GetChunkCords(globalX, globalY)].GetFloor(globalX, globalY);
    }


    //updating chunk Visuals
    public void UpdateVisableChunks(Vector2 veiwerPostion)
    {
        for (int i = 0; i < chunksLastUpdated.Count; i++)
        {
            chunksLastUpdated[i].SetVisible(false);
        }
        chunksLastUpdated.Clear();

        Vector2 currentChunkCords = GetChunkCords(veiwerPostion.x, veiwerPostion.y);

        for (int yOffset = -visableChunks; yOffset < visableChunks; yOffset++)
        {
            for (int xOffset = -visableChunks; xOffset < visableChunks; xOffset++)
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
                    //function to generate new chunk
                    chunks.Add(veiwedChunkCord, new Chunk(veiwedChunkCord, gameWorld, sprite));
                }
            }
        }

        
    }

   
}


