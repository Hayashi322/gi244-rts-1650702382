using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private bool toBuild = false; //this builder has duty to build things
    [SerializeField] private bool showGhost = false; //ghost building is showing

    [SerializeField] private GameObject[] buildingList; // Buildings that this unit can build
    public GameObject[] BuildingList { get { return buildingList; } }
    [SerializeField] private GameObject[] ghostBuildingList; // Transparent buildings according to building list

    [SerializeField] private GameObject newBuilding; // Current building to build
    public GameObject NewBuilding { get { return newBuilding; } set { newBuilding = value; } }

    [SerializeField] private GameObject ghostBuilding; // Tranparent building to check site to build
    public GameObject GhostBuilding { get { return ghostBuilding; } set { ghostBuilding = value; } }

    [SerializeField] private GameObject inProgressBuilding; // The building a unit is currently building

    public GameObject InProgressBuilding { get { return inProgressBuilding; } set { inProgressBuilding = value; } }

    private Units unit;

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Units>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
