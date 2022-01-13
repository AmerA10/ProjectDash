using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTriggerNode : MonoBehaviour, IInteractable
{
    public void HandleInteraction()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        Debug.Log("HANDLING INTERACTION");
    }

    public bool IsInteractable()
    {
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleReset()
    {
        throw new System.NotImplementedException();
    }

    public bool CanReset()
    {
        throw new System.NotImplementedException();
    }
}
