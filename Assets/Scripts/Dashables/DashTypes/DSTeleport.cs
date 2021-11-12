using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DSTeleport : IDashable
{
    private Transform teleportExit;

    public DSTeleport(Transform exit)
    {
        teleportExit = exit;

    }

    public void HandleDash(Transform player, Vector2 dashDir) {

        player.transform.position = teleportExit.position;
        
    }

}
