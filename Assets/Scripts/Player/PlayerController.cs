using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum State
{
    MOVING, DASHING, FALLING, DEAD
}
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Components")]
    private Rigidbody2D rb;
    private State playerState;
    private PlayerAnimation playerAnimation;


    public Action OnDeath;

    [Header("Moving Left and Right")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float airMoveSpeed = 20f;

    [Header("Falling")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowFallMultiplier = 2f;
    [SerializeField] private float airLerp = 5f;

    [Header("Ground Collision")]
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float checkGroundDistance = 0.5f;
    public LayerMask whatIsGround;

    [Header("Dash Stuff")]
    [SerializeField] private float dashRadius = 5f;
    [SerializeField] LayerMask whatIsDashable;
    [SerializeField] private Transform target;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.4f;
    [SerializeField] private float dashStopTime = 0.2f;

    private Vector2 dashDirection;

    public void Die()
    {
        OnDeath();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        ChangeState(State.MOVING);

    }

    // Update is called once per frame
    void Update()
    {
        //DashBehaviour();
        CheckForGround();
        
        target = CheckForDashTarget();

        if (target != null && playerState != State.DASHING)
        {
            dashDirection = GetDashDirection(target);
        }
    }

    public void Dash()
    {
        if (target == null) return;
        if (target.GetComponent<Lantern>().IsActivated()) return;
        target.GetComponent<Lantern>().Activate();
        ChangeState(State.DASHING);

        target.GetComponent<Lantern>().SetUp(this.transform);

        rb.gravityScale = 0f;
        rb.drag = 8f;

        StartCoroutine(StartDashing());

        Debug.DrawLine(this.transform.position, (Vector2)this.transform.position + (dashDirection * dashSpeed), Color.green, 1f);

    }
    IEnumerator StartDashing()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashStopTime);
     
        rb.velocity += dashDirection * dashSpeed;
        playerAnimation.DashAnim(rb.velocity);
        StartCoroutine(FinishDashing());
    }

    IEnumerator FinishDashing()
    {
        yield return new WaitForSeconds(dashTime);
    
        ChangeState(State.FALLING);
        rb.drag = 1f;
        rb.gravityScale = 1f;
        playerAnimation.EndDashAnim();
    }
    public void HandleMovementInput(float horizontalFloat)
    {
        if (isGrounded && playerState != State.DASHING)
        {
            // transform.Translate(Vector2.right * horizontalFloat * airMoveSpeed * Time.fixedDeltaTime, Space.Self);
            rb.velocity = new Vector2(horizontalFloat * moveSpeed, rb.velocity.y);
            ChangeState(State.MOVING);
        }

        else if (playerState == State.FALLING)
        {
            //The only logical next step is write custom falling physics via customizing the gravity force

            if (horizontalFloat != 0)
            {
                rb.velocity = new Vector2(horizontalFloat * airMoveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(horizontalFloat * airMoveSpeed, rb.velocity.y), airLerp * Time.deltaTime);
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb.velocity.y > 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowFallMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
        else
        {
            return;
        }
    }

    private Vector2 GetDashDirection(Transform target)
    {
        Vector2 dashDirection = (target.transform.position - this.transform.position).normalized;
        GetAngle(target, dashDirection);
        return dashDirection;
    }

    private void GetAngle(Transform target, Vector2 dir)
    {

        Debug.Log("Direction is" + dir);
        Debug.DrawLine(this.transform.position, ((Vector2)this.transform.position + dir * 5f), Color.yellow);
 
    }

    
    private void CheckForGround()
    {
        isGrounded = Physics2D.Raycast(this.transform.position, Vector2.down, checkGroundDistance, whatIsGround);
        if(!isGrounded && playerState != State.DASHING)
        {
            if (playerState == State.FALLING) return;
            
            ChangeState(State.FALLING);
        }
    }

    private Transform CheckForDashTarget()
    {
        RaycastHit2D[] colliders;
        colliders = Physics2D.CircleCastAll(this.transform.position, dashRadius, Vector2.zero, 0f, whatIsDashable);

        if (colliders.Length == 1)
        {
            return colliders[0].transform;
        }
        else if (colliders.Length > 1)
        {
            return FindClosestTarget(colliders);
        }

        return null;   
    }

    private Transform FindClosestTarget(RaycastHit2D[] targets)
    {
        Transform closest = targets[0].transform;
        foreach(RaycastHit2D hit in targets)
        {
            if((hit.transform.position - this.transform.position).sqrMagnitude < (closest.transform.position - this.transform.position).sqrMagnitude)
            {
                closest = hit.transform;
            }
        }
        return closest;
    }
    private void ChangeState(State state)
    {
        if (playerState == state) return;
        playerState = state;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Lantern>() && playerState == State.DASHING && target == collision.GetComponent<Transform>())
        {
            collision.GetComponent<Lantern>().LanternEffect(this.transform);
        }
        if(collision.tag.Equals("DeathZone"))
        {
            Die();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3.down) * checkGroundDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, dashRadius);
        Gizmos.color = Color.green;
        Gizmos.color = Color.yellow;
      
        
    }
}
