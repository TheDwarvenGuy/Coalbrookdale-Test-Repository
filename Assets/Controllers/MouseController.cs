using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Todo





public class MouseController : MonoBehaviour
{
    public static MouseController Instance { get; protected set; }

    public GameObject cursor;
    public int blueprint_direction = 1; //Direction that you will build
    public Tile selected;

    bool FoundCityMode = false;
    Tile.TileType BuildModeTile = Tile.TileType.Grass;

    Vector3 currFramePosition;
    Vector3 lastFramePosition;

    int numberOfCities = 0;

    int selected_city = 0;

    bool mouseOverMode = true;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("There should never be more than one MouseController");
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Where the mouse currently is
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        //Zoom level
        Camera.main.orthographicSize -= Input.mouseScrollDelta.y * Camera.main.orthographicSize * 0.1f;
        
        //Update camera and cursor to new value
        UpdateCameraMovement();
        UpdateCursor();

        //mouseOverMode makes the selected tile the tile you mouse over
        if (mouseOverMode)
        {
            selected = WorldController.Instance.GetTileAtWorldCoord(lastFramePosition);
            Debug.Log("Stopped moving the selected tile!");
        }
        
        //Toggle Mouse Over Mode with left click.
        if (Input.GetMouseButtonUp(1))
        {
            mouseOverMode = !mouseOverMode;
        }

        UIController.Instance.GenerateTileMenuString(selected);

        //This is only useful if I want to make multi-tile modular structures, mostly a remnant of UP-PUNK
        /*if (Input.GetKeyDown(KeyCode.E))
        {

            if (blueprint_direction < 4)
            {
                blueprint_direction++;

            }
            else if (blueprint_direction == 4)
            {
                blueprint_direction = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (blueprint_direction > 1)
            {
                blueprint_direction--;

            }
            else if (blueprint_direction == 1)
            {
                blueprint_direction = 4;
            }
        }*/


        

        if (!EventSystem.current.IsPointerOverGameObject())

        {
            if (Input.GetMouseButtonUp(0))
            {
                
                //Select a tile and keep it there
                if (FoundCityMode == false)
                {
                    mouseOverMode = false;
                    //Debug.Log("Set mouseOverMode to " + mouseOverMode);
                }

                //If Found City Mode is active, mouseOverMode will be active so that you can build a city where the selected tile is.
                //Perhaps change this system to have cities built where the selected tile is anyways, so that you can either build it on the selected tile or mouse over and click where you want to build it.
                if (FoundCityMode == true)
                {
                    mouseOverMode = true;
                    selected.Type = Tile.TileType.Building;
                    selected.Direction = blueprint_direction;

                    FoundCityOnTile(selected);
                }
            }
            
        }


        //Debug.Log(selected.x + " - " + selected.y) ;
        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;

        
    }

    void UpdateCameraMovement()
    {
        //handle screen dragging
        if (Input.GetMouseButton(2))
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }

    }

    void UpdateCursor()
    {
        //update the cursor position
        cursor.transform.position = WorldController.Instance.ToIsometric(WorldController.Instance.GetTileAtWorldCoord(lastFramePosition).x, WorldController.Instance.GetTileAtWorldCoord(lastFramePosition).y);
        cursor.transform.position -= new Vector3(0, 0, 0.1f);
    }



    //Todo: Make one function which changes build mode to an int ID
    public void SetModeMakeLand()
    {
        FoundCityMode = false;
        BuildModeTile = Tile.TileType.Grass;
    }
    public void SetModeMakeWater()
    {
        FoundCityMode = false;
        BuildModeTile = Tile.TileType.Water;
    }

    public void SetModeMakeBuilding()
    {
        FoundCityMode = false;
        BuildModeTile = Tile.TileType.Water;
    }

    public void SetMouseOverMode()
    {
        mouseOverMode = false;
    }

    public void SetModeFoundCity()
    {
        FoundCityMode = !FoundCityMode;

        Debug.Log("Toggled Buildmode to " + FoundCityMode);
    }

    void FoundCityOnTile(Tile selected)
    {
        if (selected.Town_ID == 0)
        {
            WorldController.Instance.World.numberofrooms++;
            CityController.Instance.BuildTown(selected);
            selected.Town_ID = WorldController.Instance.World.numberofrooms;
        }
    } 
}
