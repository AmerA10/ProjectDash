using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Lantern : MonoBehaviour
{
    
    // Start is called before the first frame update
    bool isActivated;
    public Action OnLanternActivation;
    private void Awake()
    {
        Reset();
    }
    
    public virtual bool IsActivated()
    {  
        return isActivated;
    }

    public virtual void Activate()
    {
        OnLanternActivation();
        //isActivated = true;
    }
    public abstract void LanternEffect(Transform player);
    public virtual void Reset()
    {
        isActivated = false;
        GetComponent<CircleCollider2D>().isTrigger = false;
    }
    public virtual void SetUp(Transform player)
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
}
