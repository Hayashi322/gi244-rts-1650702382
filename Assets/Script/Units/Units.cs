using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum UnitState
{
   Idle,
   Move,
   Attack,
   Die
}


public class Units : MonoBehaviour
{
    [SerializeField] private int id;
    public int ID { get { return id; } set { id = value; } }

    [SerializeField] private string unitName;
    public string UnitName { get { return unitName; } }

    [SerializeField] private Sprite unitPic;
    public Sprite UnitPic { get { return unitPic; } }

    [SerializeField] private int curHP;
    public int CurHP { get { return curHP; } set { curHP = value; } }

    [SerializeField] private int maxHP = 100;
    public int MaxHP { get { return maxHP; } }

    [SerializeField] private int moveSpeed = 5;
    public int MoveSpeed { get { return moveSpeed; } }

    [SerializeField] private int minWpnDamage;
    public int MinWpnDamage { get { return minWpnDamage; } }

    [SerializeField] private int maxWpnDamage;
    public int MaxWpnDamage { get { return maxWpnDamage; } }

    [SerializeField] private int armour;
    public int Armour { get { return armour; } }

    [SerializeField] private float visualRange;
    public float VisualRange { get { return visualRange; } }
    [SerializeField] private float weaponRange;
    public float WeaponRange { get { return weaponRange; } }

    [SerializeField] private UnitState state;
    public UnitState State { get { return state; } set { state = value; } }

    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get { return navAgent; } }

    [SerializeField] private Factions faction;

    [SerializeField] private GameObject selectionVisual;
    public GameObject SelectionVisual { get { return selectionVisual; } }

    

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSelectionVisual(bool flag)
    {
        if (selectionVisual != null)
            selectionVisual.SetActive(flag);
    }

}
