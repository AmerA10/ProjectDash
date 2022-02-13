using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DSDefault : IDashable
{
    public float dashSpeed;

    public DSDefault(float speed)
    {
        dashSpeed = speed;
    }
    public DSDefault() {
        dashSpeed = 100;
    }

    public void HandleDash(Transform player, Vector2 dashDirection, float dashRatio)
    {
       
        player.GetComponent<Rigidbody2D>().velocity = dashDirection * (dashSpeed * dashRatio);
        Debug.Log("x vel from dash is: " + player.GetComponent<Rigidbody2D>().velocity.x);
    }

}
