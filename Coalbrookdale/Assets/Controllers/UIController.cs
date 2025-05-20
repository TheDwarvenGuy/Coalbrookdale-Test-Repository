using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; protected set; }

    public Text right_menu_text;
    public Text left_menu_text;
    public enum menu_type {TileDisplay, CityDisplay, EconomyDisplay}

    public string tile_menu_string { get; private set; } = "";
    public string city_menu_string { get; private set; } = "";

    public Tile selected;
    public int selected_number;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("There should never be more than one UIController");
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        left_menu_text.text = selected_number + "\n";
        if (CityController.Instance.TownList.Count > 0) {
            GenerateCityMenuString(CityController.Instance.TownList[selected_number]);
        }
        left_menu_text.text += city_menu_string;
        right_menu_text.text = tile_menu_string;

    }

    public void CityCycleRight()
    {
        if (selected_number < CityController.Instance.numberOfTowns)
        {
            selected_number++;
        }
    }

    public void CityCycleLeft()
    {
        if (selected_number > 0)
        {
            selected_number--;
        }
    }


    void GenerateCityMenuString (Town selected_city)
    {
     
            city_menu_string = "Room at " + selected_city.town_center.X + ", " + selected_city.town_center.Y;
            city_menu_string += "\nID: " + selected_city.TownID;
            city_menu_string += "\nRoom type: " + selected_city;
            city_menu_string += "\nNumber of Rooms: " + selected_city.tileList.Count;

            /*
            for (int i = 0; i < CityController.Instance.TownList[selected_number].tileList.Count; i++)
            {
                city_menu_string += "\nTiles: " + CityController.Instance.TownList[selected_number].tileList[i];
            }
            */

            city_menu_string += "\n\nResources: ";
            city_menu_string += selected_city.GenerateResourceString();

            //Debug.Log("Generated City Menu String!");
    
    }
    
    public void GenerateTileMenuString(Tile selected)
    {
        tile_menu_string = ("Tile Selected in Mouse Controller: " + selected.x + ", " + selected.y
                          + "\nTile type:" + selected.Type
                          + "\nSelected Direction: " + selected.Direction
                          + "\nID: " + selected.Town_ID
                          + "\nHue: " + selected.Color);

        if (selected.Town_ID > 0)
        {
            selected_number = selected.Town_ID - 1;
        }


    }

}
