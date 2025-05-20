/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Room : IComparable<Room>
{
    public List<Tile> tileList = new List<Tile>();
    public Room(World world, Tile entrance, int ID)
    {
        this.world = world;
        this.entrance = entrance;
        this.RoomID = ID;
        this.tileList.Add(entrance);
    }

    public string name;
    public int RoomID;

    World world;
    public Tile entrance;

    public enum BuildingType {Housing, Business, Service,Road}
    public BuildingType type;

    int building_value = 1;


    public int CompareTo(Room other)
    {
        if (other == null ) return 0;
        return RoomID;
    }
}
*/