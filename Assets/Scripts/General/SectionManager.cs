using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Exit exit;
    /// <summary>
    /// <tooltip>
    /// Spawn Location of current Section</tooltip>
    /// </summary>
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private SectionManager nextSection;
    
    public Action OnSectionExit;
    void Start()
    {
        exit = GetComponentInChildren<Exit>();
        exit.OnExit += OnSectionComplete;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSectionComplete()
    {
        OnSectionExit();
    }

    public Vector3 GetSectionSpawnLocation()
    {
        return spawnLocation.position;
    }
    public SectionManager GetNextSection()
    {
        return nextSection;
    }
}
