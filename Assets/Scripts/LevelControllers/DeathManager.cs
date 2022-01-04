using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform spawnLocation;
    private PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.OnDeath += ResetPlayerPosition;
    }

    private void ResetPlayerPosition()
    {
        player.transform.position = spawnLocation.position;
    }

    public void SetSpawnLocation(Transform spawnLocation)
    {
        this.spawnLocation = spawnLocation;
    }

}
