using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyController : MonoBehaviour
{
    public static EconomyController Instance { get; protected set; }

    public int population { get; protected set; } = 0;
    public int jobsQuantity = 0;
    public int businessQuantity = 0;
    public int housingQuantity = 0;
    public int serviceQuantity = 0;

    public float housingPrice = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("There should never be more than one EconomyController");
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateEconomy()
    {
       
    }
}
