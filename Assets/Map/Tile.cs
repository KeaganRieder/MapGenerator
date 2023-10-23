using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* About
 * this class is meant to represent a tile in the game
 * a tile stores data about many thing, like the current 
 * floor type and it's effect to things like buildings
 */
public class Tile
{
    private GameObject tileObject;
    //todo add data object here 

    public Tile()
    {

    }

    public void CreateTile(int x, int y, Transform parent, Sprite sprite, Color color)
    {
        GameObject tileObject = new GameObject("tile");
        tileObject.AddComponent<SpriteRenderer>();
        tileObject.GetComponent<SpriteRenderer>().sprite = sprite;// graphic.ConvertToSprite();
        tileObject.GetComponent<SpriteRenderer>().color = color;

        tileObject.transform.SetParent(parent, false);
        tileObject.transform.position = new Vector3(x, y, 0);
    }
}

