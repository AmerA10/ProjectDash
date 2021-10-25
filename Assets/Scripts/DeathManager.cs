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
        player.OnDeath += ResetLanterns;
    }

    private void ResetLanterns()
    {
        player.transform.position = spawnLocation.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
