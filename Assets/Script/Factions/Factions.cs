using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Nation
{
    Neutral = 0,
    Braitain,
    Pirates,
    France,
    Spain,
    Portuguese,
    Dutch
}



public class Factions : MonoBehaviour
{
    [SerializeField] private Nation nation;
    public Nation Nation { get { return nation; } }
    [Header("Resources")]
    [SerializeField] private int food;
    public int Food { get { return food; } set { food = value; } }
    [SerializeField] private int wood;
    public int Wood { get { return wood; } set { wood = value; } }
    [SerializeField] private int gold;
    public int Gold { get { return gold; } set { gold = value; } }
    [SerializeField] private int stone;
    public int Stone { get { return stone; } set { stone = value; } }

    [SerializeField] private List<Units> aliveUnits = new List<Units>();
    public List<Units> AliveUnits { get { return aliveUnits; } }

    [SerializeField] private List<Building> aliveBuildings = new List<Building>();
    public List<Building> AliveBuildings { get { return aliveBuildings; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool CheckUnitCost(Units unit)
    {
        if (food < unit.UnitCost.food)
            return false;

        if (wood < unit.UnitCost.wood)
            return false;

        if (gold < unit.UnitCost.gold)
            return false;

        if (stone < unit.UnitCost.stone)
            return false;

        return true;
    }
    
    public void DeductUnitCost(Units unit)
    {
        food -= unit.UnitCost.food;
        wood -= unit.UnitCost.wood;
        gold -= unit.UnitCost.gold;
        stone -= unit.UnitCost.stone;
    }
    
    public bool IsMyUnit(Units u)
    {
        return aliveUnits.Contains(u);
    }
    
    public bool IsMyBuilding(Building b)
    {
        return aliveBuildings.Contains(b);
    }
}
