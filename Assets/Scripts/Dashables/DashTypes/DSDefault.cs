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
        dashSpeed = 85;
    }

    public void HandleDash(Transform player, Vector2 dashDirection)
    {
       
        player.GetComponent<Rigidbody2D>().velocity = dashDirection * dashSpeed;
        Debug.Log("x vel from dash is: " + player.GetComponent<Rigidbody2D>().velocity.x);
    }

}
