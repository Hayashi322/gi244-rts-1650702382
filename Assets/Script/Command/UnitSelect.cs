using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelect : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Units curUnit; //current selected single unit
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
            }
        }
    }

    private void ClearAllSelectionVisual()
    {
        if (curUnit != null)
            curUnit.ToggleSelectionVisual(false);
    }

    private void ClearEverything()
    {
        ClearAllSelectionVisual();
        curUnit = null;
    }

}
