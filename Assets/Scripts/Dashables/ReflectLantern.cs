using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectLantern : Lantern
{
    private float angleToPlayer;

    public override void SetUp(Transform player)
    {
        base.SetUp(player);
        Vector2 dir = (player.position - this.transform.position).normalized;
        angleToPlayer = Vector2.Angle(this.transform.right, dir);
    }

    public override void LanternEffect(Transform player)
    {
        //this needs more work but the idea works which is what matters
        //The way its suppose to work is that the reflection is based on where the player hits
        //from under or above it should flip the y axis velocity
        //from either sides it should flip the x axis


        //Because the bounce is based off the when it touches some math can be done before hand
        //Knowing the direction of the player should be able to give me the angle of it

        Debug.Log("Angle to playuer: " + angleToPlayer);

        if(angleToPlayer > 115 || angleToPlayer < 75)
        {
            player.GetComponent<Rigidbody2D>().velocity *= new Vector2(-1, 1);
        }
        else
        {
            player.GetComponent<Rigidbody2D>().velocity *= new Vector2(1, -1);
        }
       
       
        
    }



}


