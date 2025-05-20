using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Device;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance { get; protected set;}
    
    
    public Sprite grassSprite;
    public Sprite waterSprite;
    public Sprite highlandSprite;
    public Sprite buildingsprite;
    public Material buildingMaterial;

    public World World { get; protected set; }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
           Debug.LogError("There should never be more than one World controller");
        }
        Instance = this;
        
        
        //Create World with empty tiles
        World = new World();
        
        //create a GameObject for each tile
        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++) 
            {
                GameObject tile_go = new GameObject();
                Tile tile_data = World.GetTileAt(x, y);
                tile_go.name = "Tile_" + x + "_" + y;
                
                
                

                tile_go.transform.position = ToIsometric(tile_data.x ,tile_data.y);
                tile_go.transform.SetParent(this.transform, true);
                
                //add sprite renderer but don't bother setting sprite bc all tiles are empty
                tile_go.AddComponent<SpriteRenderer>();
                tile_go.GetComponent<SpriteRenderer>().material = buildingMaterial;
                

                // TODO: Change this all to be just a "on texture update" function.
                tile_data.RegisterTileChangedCallback((tile) => {OnTileChanged(tile, tile_go);});
            }
            //World.RandomizeTiles();
        }
        World.RandomizeTiles();
    }


    // Update is called once per frame
    void Update()
    {
        
        
        
    }

    public Vector3 ToIsometric (int x, int y)
    {
        return new Vector3 ((x - y)/2f, (x + y)/4f, x + y);
    }

    public Vector3 ToGrid (Vector3 screenPos)
    {
        return new Vector3((screenPos.x + screenPos.y * 2f), (screenPos.y * 2f - (screenPos.x)), 0);
    }


    void OnTileChanged(Tile tile_data, GameObject tile_go)
    {
        
        
        if (tile_data.Type == Tile.TileType.Grass)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = grassSprite;
        
        }
        else if(tile_data.Type == Tile.TileType.Water)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = waterSprite;
        }
        else if (tile_data.Type == Tile.TileType.Highland)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = highlandSprite;
        }
        else if (tile_data.Type == Tile.TileType.Building)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = buildingsprite;
            Debug.Log("Color changed to: " + tile_data.Color);
            tile_go.GetComponent<SpriteRenderer>().material.color = Color.HSVToRGB(tile_data.Color, 0.5f, 0.5f);
        }
        else
        {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
        }
        
        // color swapping
        if (tile_data.Town_ID !=0)
        {
            tile_data.Color = (tile_data.Town_ID % 10) / 10f;
            tile_go.GetComponent<SpriteRenderer>().material.color = Color.HSVToRGB(tile_data.Color, 0.8f, 1f - (tile_data.Town_ID % 50 / 100f));
        }
        //tile_go.transform.rotation = Quaternion.Euler(0, 0, (tile_data.Direction - 1) * -90f); //replace this with isometric tile swapping once a system is worked out







    }

    public Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(ToGrid(coord).x + 0.5f);
        int y = Mathf.FloorToInt(ToGrid(coord).y+ 0.5f);



        if (WorldController.Instance.World.GetTileAt(x, y) != null)
        {
            //Debug.Log("Tile Selected in Mouse Controller: " + x + ", " + y);
            return WorldController.Instance.World.GetTileAt(x, y);
        }
        else if (WorldController.Instance.World.GetTileAt(x, y) == null)
        {
            //Debug.Log("Tile returned Null");
            return WorldController.Instance.World.GetTileAt(0, 0);
        }
        else
        {
            Debug.Log("something funny up yo");
            return WorldController.Instance.World.GetTileAt(0, 0);
        }
    }
}
