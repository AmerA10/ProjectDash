using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDash : Dashable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Awake()
    {
        if(GetComponent<Health>() != null)
        dashType = new DSDamage(GetComponent<Health>());

        defaultDash = new DSDefault();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
