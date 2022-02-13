using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSDamage : IDashable
{

    private Health health;
    // Start is called before the first frame update

    public DSDamage(Health health)
    {
        this.health = health;
    }
    
    public void HandleDash(Transform player, Vector2 dashDir, float dashRatio)
    {
        health.TakeDamage(1);
    }
}
