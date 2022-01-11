using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Exit[] exits;
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

    }

    public void TeleportPlayer(Exit destination) {
        OnSectionTeleport(destination, destination.GetSection());
    }

}
