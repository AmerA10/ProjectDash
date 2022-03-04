using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DSProjectileStart : IDashable
{
    // Start is called before the first frame update
    public float dashSpeed;
    private CircleCollider2D cirlce;
    public Action OnDashEvent;

    public DSProjectileStart(CircleCollider2D cirlce, float speed)
    {
        dashSpeed = speed;
        this.cirlce = cirlce;
    }

    public void HandleDash(Transform player, Vector2 dashDirection, float dashRatio)
    {
        //cirlce.enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = dashDirection * (dashSpeed * dashRatio);
        OnDashEvent();
    }
    
}
