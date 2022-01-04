using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
[RequireComponent(typeof(BoxCollider2D))]
public class Exit : MonoBehaviour, IInteractable
{

    [SerializeField] private Exit destination;
    [SerializeField] private SectionManager section;


    private void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
       // if (sprite) sprite.enabled = false;
    }


    public void SetSection(SectionManager section)
    {
        this.section = section;
    }

    public SectionManager GetSection()
    {
        return section;
    }

    public Exit GetDesitnation()
    {
        return destination;
    }

    public void HandleInteraction()
    {
        section.TeleportPlayer(destination);
    }

    public bool IsInteractable()
    {
        return true;
    }
}
