using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* about TileData
 * class thats added to a tile game object upon creation 
 * and holds the data for that tile like stats 
 */
public class TileData<DataType> : MonoBehaviour
{
    private DataType tileData;

    public void SetData(DataType data)
    {
        tileData = data;
    }
    public DataType GetData()
    {
        return tileData;
    }
    
}

public class tempClass
{
    public string name;
}
