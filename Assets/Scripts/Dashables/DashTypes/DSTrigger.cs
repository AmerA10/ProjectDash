using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSTrigger : IDashable
{
    IInteractable inter;
    public DSTrigger(IInteractable interactable)
    {
        inter = interactable;
    }

    public void HandleDash(Transform player, Vector2 direction, float dashRatio)
    {
        if (inter == null)
        {
            Debug.LogWarning("The trigger node has no game object with IIinteractable in it");
            return;
        }
        if(inter.IsInteractable())
        {
            inter.HandleInteraction();
        }
       
    }



}