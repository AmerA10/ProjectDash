using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ProjectileDash : Dashable
{
    CircleCollider2D circle;
    [SerializeField] float speed = 100f;
    [SerializeField] float timerToResetTag = 1f;
    string defaultTag;
    // Start is called before the first frame update
    void Awake()
    {
        defaultTag = this.tag;
        circle = GetComponent<CircleCollider2D>();
        defaultDash = new DSProjectileStart(circle, speed);
        DSProjectileStart dashHolder = (DSProjectileStart)defaultDash;
        dashType = null;
        dashHolder.OnDashEvent += OnDash;
    }

    public void OnDash()
    {
        this.transform.tag = "Dashable";
        Invoke(nameof(ResetTag), timerToResetTag);
    }
    private void ResetTag()
    {
        this.transform.tag = defaultTag;
    }

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
