using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelect : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Units curUnit; //current selected single unit
    [SerializeField]
    private Building curBuilding; //current selected single building
    public Building CurBuilding { get { return curBuilding; } }
    public Units CurUnit { get { return curUnit; } }

    private Camera cam;
    private Factions faction;

    public static UnitSelect instance;

    void Awake()
    {
        faction = GetComponent<Factions>();
    }


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        layerMask = LayerMask.GetMask("Unit", "Building", "Resource", "Ground");

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //mouse down
        if (Input.GetMouseButtonDown(0))
        {
            //if click UI, don't clear
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            ClearEverything();
        }

        // mouse up
        if (Input.GetMouseButtonUp(0))
        {
            TrySelect(Input.mousePosition);
        }
    }

    private void SelectUnit(RaycastHit hit)
    {
        curUnit = hit.collider.GetComponent<Units>();

        curUnit.ToggleSelectionVisual(true);

        Debug.Log("Selected Unit");

        if (GameManager.instance.MyFaction.IsMyUnit(curUnit))
        { 
            ShowUnit(curUnit);
        }
    }

    private void TrySelect(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;

        //if we left-click something
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            switch (hit.collider.tag)
            {
                case "Unit":
                    SelectUnit(hit);
                    break;
                case "Building":
                    BuildingSelect(hit); 
                    break;
            }
        }
    }

    private void ClearAllSelectionVisual()
    {
        if (curUnit != null)
            curUnit.ToggleSelectionVisual(false);
        if (curBuilding != null)
            curBuilding.ToggleSelectionVisual(false);
    }

    private void ClearEverything()
    {
        ClearAllSelectionVisual();
        curUnit = null;
        curBuilding = null;

        //Clear UI
        InfoManager.instance.ClearAllInfo();
        ActionManager.instance.ClearAllInfo();
    }

    private void ShowUnit(Units u)
    {
        InfoManager.instance.ShowAllInfo(u);
    }
    private void ShowBuilding(Building b)
    {
        InfoManager.instance.ShowAllInfo(b);
        ActionManager.instance.ShowCreateUnitMode(b);
    }
    private void BuildingSelect(RaycastHit hit)
    {
        curBuilding = hit.collider.GetComponent<Building>();
        curBuilding.ToggleSelectionVisual(true);

        if (GameManager.instance.MyFaction.IsMyBuilding(curBuilding))
        {
            //Debug.Log("my building");
            ShowBuilding(curBuilding);//Show building info
        }
    }
}
