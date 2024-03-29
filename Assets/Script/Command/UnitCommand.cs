using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCommand : MonoBehaviour
{
    public LayerMask layerMask;
    private UnitSelect unitSelect;

    private Camera cam;

    void Awake()
    {
        unitSelect = GetComponent<UnitSelect>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        layerMask = LayerMask.GetMask("Unit", "Building", "Resource", "Ground");
    }

    // Update is called once per frame
    void Update()
    {
        // mouse up
        if (Input.GetMouseButtonUp(1))
        {
            TryCommand(Input.mousePosition);
        }

    }

    private void UnitsMoveToPosition(Vector3 dest, List<Units> units)
    {
        foreach (Units u in units)
        { 
            if (u != null)
             u.MoveToPosition(dest); 

        }
    }

    private void CommandToGround(RaycastHit hit, List<Units> units)
    {
        UnitsMoveToPosition(hit.point, units);
        CreateVFXMarker(hit.point, MainUI.instance.SelectionMarker);
    }

    private void TryCommand(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;

        //if we left-click something
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            switch (hit.collider.tag)
            {
                case "Ground":
                    CommandToGround(hit, unitSelect.CurUnit);
                    break;
                case "Resource":
                    ResourceCommand(hit, unitSelect.CurUnit);
                    break;
                case "Unit":
                    CommandToUnit(hit, unitSelect.CurUnit);
                    break;
                case "Building":
                    BuildingCommand(hit, unitSelect.CurUnit);
                    break;
            }
        }
    }

    private void CreateVFXMarker(Vector3 pos, GameObject vfxPrefab)
    {
        if (vfxPrefab == null)
            return;

        Instantiate(vfxPrefab, new Vector3(pos.x, 0.1f, pos.z), Quaternion.identity);
    }
    // called when we command units to gather a resource
    private void UnitsToGatherResource(Resourcesource resource, List<Units> units)
    {
        foreach (Units u in units)
        {
            if (u.IsWorker)
                u.Worker.ToGatherResource(resource, resource.transform.position);
            else
                u.MoveToPosition(resource.transform.position);
        }
        
    }
    private void ResourceCommand(RaycastHit hit, List<Units> units)
    {
        UnitsToGatherResource(hit.collider.GetComponent<Resourcesource>(), units);
        CreateVFXMarker(hit.transform.position, MainUI.instance.SelectionMarker);
    }
    private void UnitAttackEnemy(Units enemy, List<Units> units)
    {
        foreach (Units u in units)
        {
            u.ToAttackUnit(enemy);
        }
    }
    private void CommandToUnit(RaycastHit hit, List<Units> units)
    {
        Units target = hit.collider.gameObject.GetComponent<Units>();

        if (target == null)
            return;

        if (target.Faction == GameManager.instance.EnemyFaction)// if it is our enemy
            UnitAttackEnemy(target, units);
    }
    private void UnitAttackEnemyBuilding(Building enemyBuilding, List<Units> units)
    {
        foreach (Units u in units)
        {
            u.ToAttackBuilding(enemyBuilding);
        }
    }
    private void BuildingCommand(RaycastHit hit, List<Units> units)
    {
        Building building = hit.collider.gameObject.GetComponent<Building>();

        if (building == null)
            return;

        // if it is an enemy's building
        if (building.Faction == GameManager.instance.EnemyFaction)
            UnitAttackEnemyBuilding(building, units);
    }
}
