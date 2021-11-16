using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chain : MonoBehaviour
{

    [SerializeField] private float delta;
    [SerializeField] private bool isHit;
    [SerializeField] public bool canHit = false;
    [SerializeField] private Transform hookShotEnd;
    [SerializeField] private Transform target;
    [SerializeField] private float hookShotRadius = 1f;
    [SerializeField] LayerMask whatIsDashable;
    [SerializeField] private ChainTrail trail;
    public Action OnHookHit;

    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
        SetTrailActive(false);
    }


    public void ShootHookTo(Transform target)
    {
        SetTrailActive(true);
        this.transform.parent = null;
        Vector2 directionToTarget = target.position - this.transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.target = target;
        isHit = false;
        StartCoroutine(MoveToTarget());

    }
    IEnumerator MoveToTarget()
    {
        RaycastHit2D hit;

        while (!isHit)
        {
            hit = Physics2D.CircleCast(hookShotEnd.position, hookShotRadius, Vector2.zero, 0f, whatIsDashable);
            transform.position = Vector2.MoveTowards(this.transform.position, target.position, delta * Time.deltaTime);
            if (hit && canHit)
            {
                isHit = true;
                OnHookHit();
            }

            yield return null;

        }

    }
    public void SetTrailActive(bool isActive)
    {
        if (isActive)
            trail.StartComputing();
        else trail.StopComputing();
  
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hookShotEnd.position, hookShotRadius);
    }

}


