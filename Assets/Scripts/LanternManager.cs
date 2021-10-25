using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LanternManager : MonoBehaviour
{

    //gonna need the singleton pattern probably
    // Start is called before the first frame update

    int lanternAmount;
    public Lantern[] lanterns;
    public Action OnDoorOpen;


    private void Awake()
    {

        lanterns = FindObjectsOfType<Lantern>();
        SetUpPlayerEvents();
        SetUpLanternEvent();
 
    }

    private void SetUpLanternEvent()
    {
        foreach(Lantern lantern in lanterns)
        {
            lantern.OnLanternActivation += OnLanternActivation;
        }
    }
    private void SetUpPlayerEvents()
    {
        FindObjectOfType<PlayerController>().OnDeath += ResetLanterns;
    }

    public void ResetLanterns()
    {
        foreach (Lantern lantern in lanterns)
        {
            lantern.Reset();
        }
    }
    private void OnLanternActivation()
    {
        Debug.Log("Lantern activated");
        lanternAmount++;
        Debug.Log(lanternAmount + " /" + lanterns.Length);
        CheckLanternAmount();
    }

    private void CheckLanternAmount()
    {
        if(lanternAmount >= lanterns.Length)
        {
            Debug.Log("All lanterns activated, opening door");
            this.OnDoorOpen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
