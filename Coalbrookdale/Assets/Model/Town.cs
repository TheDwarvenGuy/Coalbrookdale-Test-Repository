using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Town : IComparable<Town>
{
    public List<Tile> tileList = new List<Tile>();

    public List<Building> buildingList = new List<Building>();

    public Town(World world, Tile town_center, int ID)
    {
        this.world = world;
        this.town_center = town_center;
        this.TownID = ID;
        this.tileList.Add(town_center);
        AddTilesSquare();
    }

    public string name;
    public int TownID;

    World world;
    public Tile town_center;

    

    //resources
    int ore = 0; int ore_price = 0;
    int food = 0; int food_price = 0;
    int tools = 0; int tools_price = 0;
    int groceries = 0; int groceries_price = 0;

    // pops
    int laborers = 0;
    int artisans = 0;
    int capitalists = 0;


    public int CompareTo(Town other)
    {
        if (other == null ) return 0;
        return TownID;
    }

    public void updateYields()
    {
        //TODO: Create tile yields at the tile level and find add the yields from the tile instead


        ore = 0; ore_price = 0;
        food = 0; food_price = 0;
        tools = 0; tools_price = 0;
        groceries = 0; groceries_price = 0;


        for (int i = 0; i < tileList.Count; i++)
        {
            //Debug.Log("Began updating! Tile list size: " + tileList.Count);
            if (tileList[i].Type == Tile.TileType.Highland) { ore += 2;  food += 1; /*Debug.Log("Detected Highland!")*/; } 
            if (tileList[i].Type == Tile.TileType.Grass) { ore += 1; food += 2; /*Debug.Log("Detected Grass!)*/; }
            if (tileList[i].Type == Tile.TileType.Water) { food += 3; /*Debug.Log("Detected Water!")*/; }
            if (tileList[i].Type == Tile.TileType.Building) { food -= 4; tools += 2 /*Debug.Log("Detected Water!")*/; }
        }

        /*Debug.Log("Updated Yields for! " + TownID + "\n ore: " + ore + "\nfood: " + food)*/;
    }

    public string GenerateResourceString()
    {
       string resourceString = new string("");

        resourceString += "\nore: " + ore;
        resourceString += "\nfood: " + food;
        resourceString += "\ntools: " + tools;
        resourceString += "\ngroceries: " + groceries;

        //Debug.Log("Generated Resource String!");

        return resourceString;
    }

    public void AddTilesSquare()
    {
        for (int i = -3; i <= 3; i++)
        {
            for (int j = -3; j <= 3; j++)
            {
                int x = town_center.X + i;
                int y = town_center.Y + j;

                if (x >= 0 && y >= 0 && x < world.Width && y < world.Height)
                {
                    if (world.GetTileAt(x, y).Town_ID == 0)
                    {
                        world.GetTileAt(x, y).Town_ID = TownID;
                        tileList.Add(world.GetTileAt(x, y));
                    }    
                }
            }
        }

        Debug.Log("Added tiles in square shape");
    }
}
