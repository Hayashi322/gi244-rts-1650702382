using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{   [SerializeField]
    private GameObject[] woodTreePrefab;

    [SerializeField]
    private Transform woodTreeParent;

    [SerializeField]
    private Resourcesource[] resources;

    public static ResourceManager instance;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        FindAllResource();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FindAllResource()
    {
        resources = FindObjectsOfType<Resourcesource>();
    }
}
