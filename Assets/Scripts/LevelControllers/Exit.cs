using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
[RequireComponent(typeof(BoxCollider2D))]
public class Exit : MonoBehaviour, IInteractable
{

   
    public Action OnExit;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite) sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You completed the level");
        OnExit();

    }

    public void HandleInteraction()
    {
        throw new NotImplementedException();
    }

    public bool IsInteractable()
    {
        throw new NotImplementedException();
    }
}
