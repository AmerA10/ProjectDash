using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDash : MonoBehaviour, IInteractable
{
    public GameObject exit;

    public GameObject lockedDoor;

    private void Start()
    {
        exit.SetActive(false);
        lockedDoor.SetActive(true);
    }

    public void HandleInteraction()
    {
        exit.SetActive(true);
        lockedDoor.SetActive(false);
    }

    public bool IsInteractable()
    {
        return !exit.activeSelf;
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
