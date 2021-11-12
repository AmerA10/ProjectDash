using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDash : Dashable
{
    // Start is called before the first frame update

    [SerializeField] Transform exit;

    private void Awake()
    {
        dashType = new DSTeleport(exit);
        defaultDash = new DSDefault();

    }

}
