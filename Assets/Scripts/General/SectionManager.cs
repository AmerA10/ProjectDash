using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Exit[] exits;
    [SerializeField] private Vector2 xCamClamp;
    [SerializeField] private Vector2 yCamClamp;
    [SerializeField] private IInteractable[] interactables;
    [SerializeField] private Transform spawnLocation;

    /// <summary>
    /// <tooltip>
    /// Spawn Location of current Section</tooltip>
    /// </summary>


    public Action<Exit, SectionManager> OnSectionTeleport;
    void Awake()
    {
        exits = GetComponentsInChildren<Exit>();
        foreach (Exit exit in exits) {
            exit.SetSection(this);
        }

        interactables = GetComponentsInChildren<IInteractable>();
    }
    public void TeleportPlayer(Exit destination) {
        Debug.Log("Teleporting to : " + destination);
        Debug.Log("In the section : " + destination.GetSection());
        OnSectionTeleport(destination, destination.GetSection());
    }

    public void ResetIInteractables()
    {
        foreach (IInteractable inter in interactables)
        {
            if(inter.CanReset()) inter.HandleReset();
        }
    }

    public Transform GetSpawnLocation()
    {
        return spawnLocation;
    }
    
    public Vector2 GetXClamp()
    {
        return xCamClamp;
    }
    public Vector2 GetYClamp()
    {
        return yCamClamp;
    }

    public void SetClamps(Vector2 xClamp, Vector2 YClamps)
    {
        xCamClamp = xClamp;
        yCamClamp = YClamps;
    }

}
