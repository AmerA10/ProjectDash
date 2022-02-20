using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallHazard : MonoBehaviour, IHazard, IReset
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform stopPosition;
    [SerializeField] float distanceToStop = 1f;
    private bool canMove = false;
    private Vector2 startingPos;

    

    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        startingPos = this.transform.position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) MoveToDestination();
        
    }

    private void MoveToDestination()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, stopPosition.position, Time.deltaTime * moveSpeed);
        CheckDestination();
    }
    private void CheckDestination()
    {
        if ((stopPosition.position - this.transform.position).sqrMagnitude < distanceToStop) StopHazard();

    }

    public void StartHazard()
    {
        stopPosition.transform.parent = null;
        boxCollider.enabled = true;
        canMove = true;
    }
    public void StopHazard()
    {
        stopPosition.transform.parent = this.transform;
        boxCollider.enabled = false;
        canMove = false;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(this.transform.position, stopPosition.position);
        Gizmos.DrawWireSphere(stopPosition.transform.position, 1f);
    }

    public void HandleReset()
    {
        stopPosition.transform.parent = this.transform;
        this.transform.position = startingPos;
        canMove = false;
        boxCollider.enabled = false;
    }
    public bool isResettable()
    {
        return true;
    }
}
