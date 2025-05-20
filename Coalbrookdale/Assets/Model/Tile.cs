using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Tile
{
    public enum TileType{ Empty, Water, Grass, Highland, Building};
    int direction = 1;
    int room_ID = 0; //ID which tells what room a tile belongs to
    float color = 0.5f;
    bool isEntrance = false;
    
    
    TileType type = TileType.Empty;
    
    Action<Tile> cbTileChanged;
    public TileType Type
    {
        get
        {
            return type;
        }
        set
        {
            TileType oldType = type;
            type = value;
            //Call the callback and let things know we've changed
            if (cbTileChanged != null && oldType != type)
            {
                cbTileChanged(this);
            }
        }
        
        
    }

    public int Direction
    {
        get
        {
            return direction;
        }
        set
        {
            int oldDirection = direction;
            direction = value;

            if (cbTileChanged != null && oldDirection != direction)
            {
                cbTileChanged(this);
            }
        }

    }

    public float Color
    {
        get
        {
            return color;
        }
        set
        {
            

            float oldColor = color;
            color = value;

            //Debug.Log("Attempted to make color " + color);

            if (cbTileChanged != null && oldColor != color)
            {
                cbTileChanged(this);
            }
        }

    }

    public int Town_ID
    {
        get
        {
            return room_ID;
        }
        set
        {
            int oldRoomID = room_ID;
            room_ID = value;

            if (cbTileChanged != null && oldRoomID != Town_ID)
            {
                cbTileChanged(this);
            }
        }

    }


    Building building;
    
    World world;
    public int    x {get; protected set;}
    public int    y {get; protected set;}


    public int X 
    {
        get 
        {
            return x;
        }
    }
    
    public int Y
    {
        get
        {
            return y;
        }
    }

    public Tile(World world, int x, int y) 
    {
        this.world = world;
        this.x = x;
        this.y = y; 
    }
    
    public void RegisterTileChangedCallback(Action<Tile> callback)
    {
        cbTileChanged += callback;
    }
    
    public void UnRegisterTileChangedCallback(Action<Tile> callback)
    {
        cbTileChanged -= callback;
    }
}
