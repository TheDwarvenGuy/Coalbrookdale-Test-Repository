using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Reflection;

public class CityController : MonoBehaviour
{
    public static CityController Instance { get; protected set; }

    public Text city_menu_text;
    public Sprite citySprite;

    public List<Town> TownList = new List<Town>();
    public int numberOfTowns = 0;
    int selected_city = 0;
    



    




    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("There should never be more than one CityController");
        }
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildTown(Tile selected)
    {
        Debug.Log("Town built!");
        
        numberOfTowns++;

        GameObject town_go = new GameObject();
        Town town_data = new Town(WorldController.Instance.World, selected,numberOfTowns);
        //town_data.type = blueprintType;

        town_go.name = "Town " + numberOfTowns;
        town_go.AddComponent<SpriteRenderer>().sprite = citySprite;
        town_go.transform.position = WorldController.Instance.ToIsometric(selected.x, selected.y);
        town_go.transform.position -= new Vector3(0, 0, 0.1f);
        town_go.transform.SetParent(this.transform, true);
        TownList.Add(town_data);
    }

    public void UpdateAllYields()
    {
        for (int i = 0; i < TownList.Count; i++)
        {
            TownList[i].updateYields();
        }
    }

    


    public void SetTypeHousing()
    {
        //blueprintType = City.BuildingType.Baker;
    }
    public void SetTypeBusiness()
    {
        //blueprintType = City.BuildingType.Blacksmith;
    }
    public void setTypeRoad()
    {
        //blueprintType = City.BuildingType.Town_Center;
    }
    public void setTypeService()
    {
        //blueprintType = City.BuildingType.Housing;
    }

}
