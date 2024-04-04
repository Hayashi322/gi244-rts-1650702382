using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Structure
{
    [SerializeField] private Transform spawnPoint;
    public Transform SpawnPoint { get { return spawnPoint; } }
    [SerializeField] private Transform rallyPoint;
    public Transform RallyPoint { get { return rallyPoint; } }

    [SerializeField] private GameObject[] unitPrefabs;
    public GameObject[] UnitPrefabs { get { return unitPrefabs; } }

    [SerializeField] private List<Units> recruitList = new List<Units>();

    [SerializeField] private float unitTimer = 0f;
    [SerializeField] private int curUnitProgress = 0;

    [SerializeField] private float curUnitWaitTime = 0f;

    [SerializeField] private bool isFunctional;
    public bool IsFunctional { get { return isFunctional; } set { isFunctional = value; } }

    [SerializeField] private bool isHQ;
    public bool IsHQ { get { return isHQ; } }

    [SerializeField] private bool isHousing;
    public bool IsHousing { get { return isHousing; } }

    [SerializeField] private bool isBarrack;
    public bool IsBarrack { get {  return isBarrack; } }

    // How deep into the ground the building is at the construction site
    [SerializeField] private float intoTheGround = 5f;
    public float IntoTheGround { get { return intoTheGround; } }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            ToCreateUnit(0);

        if (Input.GetKeyDown(KeyCode.H))
            ToCreateUnit(1);

        if ((recruitList.Count > 0) && (recruitList[0] != null))
        {
            unitTimer += Time.deltaTime;
            curUnitWaitTime = recruitList[0].UnitWaitTime;

            if (unitTimer >= curUnitWaitTime)
            {
                curUnitProgress++;
                unitTimer = 0f;

                if (curUnitProgress >= 100 && (faction.AliveUnits.Count < faction.UnitLimit))
                {
                    curUnitProgress = 0;
                    curUnitWaitTime = 0f;
                    CreateUnitCompleted();
                }
            }
        }
    }
    
    public void ToCreateUnit(int i)
    {
        Debug.Log(structureName + " creates " + i + ":" + unitPrefabs.Length);
        if (unitPrefabs.Length == 0)
            return;

        if (unitPrefabs[i] == null)
            return;

        Units unit = unitPrefabs[i].GetComponent<Units>();

        if (unit == null) 
            return;

        if (!faction.CheckUnitCost(unit)) //not enough resources
            return;

        //Deduct Resource
        faction.DeductUnitCost(unit);

        //If it's me, update UI
        if (faction == GameManager.instance.MyFaction)
            MainUI.instance.UpdateAllResource(faction);

        //Add unit into faction's recruit list
        recruitList.Add(unit);

        Debug.Log("Adding" + i + "to Recruit List");
    }
    
    public void CreateUnitCompleted()
    {
        int id = recruitList[0].ID;

        if (faction.UnitPrefabs[id] == null)
            return;

        GameObject unitObj = Instantiate(faction.UnitPrefabs[id], spawnPoint.position, Quaternion.Euler(0f, 180f, 0f),faction.UnitsParent);

        recruitList.RemoveAt(0);

        Units unit = unitObj.GetComponent<Units>();
        unit.Faction = faction; 
        unit.MoveToPosition(rallyPoint.position); //Go to Rally Point

        //Add unit into faction's Army
        faction.AliveUnits.Add(unit);

        Debug.Log("Unit Recruited");
        //If it's me, update UI
        if (faction == GameManager.instance.MyFaction)
            MainUI.instance.UpdateAllResource(faction);
    }
    
    public void ToggleSelectionVisual(bool flag)
    {
        if (SelectionVisual != null)
            SelectionVisual.SetActive(flag);
    }
    private float timer = 0f; //Constructing timer
    public float Timer { get { return timer; } set { timer = value; } }
    private float waitTime = 0.5f; //How fast it will be construct, higher is longer

    public float WaitTime
    {
        get { return waitTime; } set { waitTime = value; } 
        
    }
    public int CheckNumInRecruitList(int id)
    {
        int num = 0;

        foreach (Units u in recruitList)
        {
            if (id == u.ID)
                num++;
        }
        return num;
    }
    protected override void Die()
    {
        if (faction != null)
            faction.AliveBuildings.Remove(this);

        if (IsHousing)
            faction.UpdateHousingLimit();

        base.Die();

        //Check Victory Condition
    }
}
