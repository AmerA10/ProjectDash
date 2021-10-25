using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTarget : MonoBehaviour, IShootable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShot()
    {
        Debug.Log("I got shot");
    }
}
