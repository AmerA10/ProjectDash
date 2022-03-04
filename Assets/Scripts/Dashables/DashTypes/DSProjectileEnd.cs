using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSProjectileEnd : IDashable
{
    // Start is called before the first frame update
    private CircleCollider2D circle;
    float timer;
    public DSProjectileEnd(CircleCollider2D circle, float timer)
    {
        this.timer = timer;
        this.circle = circle;
    }

    public void HandleDash(Transform player, Vector2 dashDirection, float dashRatio)
    {
        
        StartBox();
    
    }
    private void StartBox()
    {
        float timeToInvoke = 0f;
        while (timeToInvoke < timer)
        {
            Debug.Log("What");
            timeToInvoke += Time.deltaTime;
        }
        circle.enabled = true;

    }
}
