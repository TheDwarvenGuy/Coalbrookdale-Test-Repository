using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public int Width
    {
        get 
        {
            return width;
        }
    }
    
    public int Height
    {
        get 
        {
            return height;
        }
    }
    
    Tile[,] tiles;
    int width;
    int height;
    public int numberofrooms = 0;


    public World(int width = 100, int height = 100)
    {
        this.width = width;
        this.height = height;
        tiles = new Tile[width,height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                tiles[x,y] = new Tile(this, x, y);
            }
        }
        
        Debug.Log("World created with " + (width * height) + " tiles");
    }
    
    public void RandomizeTiles()
    {
        Debug.Log("Randomized Tiles");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                if (Random.Range(0, x / 2) == 0 || Random.Range(0, (100 - x)/2) == 0)
                {
                    tiles[x,y].Type = Tile.TileType.Water;
                    
                }
                else if (Random.Range(0, 2) == 1)
                {
                    tiles[x,y].Type = Tile.TileType.Highland;
                    
                }
                else
                {
                    tiles[x, y].Type = Tile.TileType.Grass;

                }
            }
        }
    }
    
    public Tile GetTileAt(int x, int y) 
    {
        if (x >= width || x < 0 || y >= height || y < 0)
        {
            //Debug.LogError("Tile: (" +x+ ", " +y+ ") is out of range");
            return null;
        }
        return tiles[x,y];
    }


    public Tile GetTileFacing(int x, int y, int direction)
    {
        int facingx = x;
        int facingy = y;
        if (x >= width || x < 0 || y >= height || y < 0)
        {
            Debug.LogError("Tile: (" + x + ", " + y + ") is out of range");
            return null;
        }

        if (direction == 1)
        {
            facingy++;
        }
        else if (direction == 3)
        {
            facingy--;
        }
        else if (direction == 2)
        {
            facingx++;
        }
        else if (direction == 4)
        {
            facingx--;
        }
        
        if (facingx >= width || facingx < 0 || facingy >= height || facingy < 0)
        {
            return null;
        }
        if (facingx == x && facingy == y)
        {
            Debug.LogError("Guhh?? A tile shouldn't be able to face itself");
            return null;
        }


        return tiles[facingx, facingy];
    }

}
