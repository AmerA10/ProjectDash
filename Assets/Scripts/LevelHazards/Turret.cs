using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IHazard, IReset
{
    [SerializeField] GameObject projectile;
    [SerializeField] float timeToShoot;
    Vector2 shootDir;

    bool canShoot = true;

    public void HandleReset()
    {
        throw new System.NotImplementedException();
    }

    public bool isResettable()
    {
        throw new System.NotImplementedException();
    }

    public void StartHazard()
    {
        throw new System.NotImplementedException();
    }

    public void StopHazard()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        shootDir = this.transform.forward;
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }



}
