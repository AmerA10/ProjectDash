using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
[RequireComponent(typeof(BoxCollider2D))]
public class Exit : MonoBehaviour
{
    public Action OnLevelComplete;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = true;
       
    }
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnLevelComplete();
        Debug.Log("You completed the level");
    }


}
