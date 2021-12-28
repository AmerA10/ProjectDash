using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
[RequireComponent(typeof(BoxCollider2D))]
public class Exit : MonoBehaviour
{

    [SerializeField] private Fader fader;
    public Action OnExit;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        Debug.Log("ON!");
       
    }
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You completed the level");
        OnExit();

    }



}
