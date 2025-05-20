using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    public enum BuildingType { Blacksmith, Baker, Town_Center, Housing }
    public BuildingType type;

    World world = WorldController.Instance.World;
    Tile tile;
    Town city;
    public Building(Town city, Tile tile)
    {
        this.city = city;
        this.tile = tile;
    }
}
