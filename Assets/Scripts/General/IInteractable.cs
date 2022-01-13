using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void HandleInteraction();
    bool IsInteractable();

    void HandleReset();
    bool CanReset();
}
