using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDash : Dashable
{
    // Start is called before the first frame update

    [SerializeField] float dashSpeed = 100f;

    private void Awake()
    {
         defaultDash = new DSDefault(dashSpeed);
    }

}
