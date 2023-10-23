using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* about ChunkData
 * defines a games chunk, which is something to store object/data in
 * and allow for unlaoding/loading of it
 */
public class Chunk 
{
    public const int CHUNK_SIZE = 32;
    private tempClass[,] chunkContents = new tempClass[CHUNK_SIZE, CHUNK_SIZE];

    public Chunk() {

    }

    public void SetFloor(int x, int y, tempClass data)
    {
        chunkContents[x, y] = data;
    }

}
