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

    private void UnitsMoveToPosition(Vector3 dest, Units unit)
    {
        if (unit != null)
            unit.MoveToPosition(dest);
    }

    private void CommandToGround(RaycastHit hit, Units unit)
    {
        UnitsMoveToPosition(hit.point, unit);
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
            }
        }
    }
}
