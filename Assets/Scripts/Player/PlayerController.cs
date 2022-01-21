using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DashEnums;



public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField] private State playerState;
    private PlayerAnimation playerAnimation;
    private PlayerInput playerInput;

    public Action OnDeath;

    [Header("Moving Left and Right")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float airMoveSpeed = 20f;
    private bool isRight = true;

    [Header("Falling")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowFallMultiplier = 2f;
    [SerializeField] private float airLerp = 5f;

    [Header("Ground Collision")]
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float checkGroundDistance = 0.05f;
    [SerializeField] private Transform groundCheckTransform;
    public LayerMask whatIsGround;


    [Header("Dash Stuff")]
    [SerializeField] private float dashRadius = 5f;
    [SerializeField] LayerMask whatIsDashable;
    [SerializeField] private Transform target;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.4f;
    [SerializeField] private float dashStopTime = 0.2f;
    [SerializeField] private Chain hook;
    [SerializeField] private Dashable dashStrat;
    [SerializeField] private bool canDash = true;
    private bool isHookCaught = false;
    private Vector2 dashDirection;

    [Header("Jump Stuff")]
    [SerializeField] private float jumpForce = 10f;

    public Action<bool> TimeAction;

    public void Die()
    {
        rb.velocity = Vector2.zero;
        canDash = true;
        OnDeath();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerInput = GetComponent<PlayerInput>();
        ChangeState(State.IDLE_RIGHT);
        hook.OnHookHit += Dash;
        GrabHook();
    }

    void Update()
    {
        CheckForGround();

        if (canDash) target = CheckForDashTarget();
        AdjustPlayerState();

        if (target != null && canDash)
        {
            dashStrat = target.GetComponent<Dashable>();
            dashDirection = GetDashDirection(target);
            TimeAction?.Invoke(true);
            //TurnTime(true);
        }

    }

    private void AdjustPlayerState()
    {
        if (playerState == State.SHOOT_LEFT || playerState == State.SHOOT_RIGHT) return;
        
        if (isGrounded &&  playerState != State.JUMPING)
        {
            float inputDir = playerInput.GetPlayerInputDirection();
            if (inputDir > 0 && playerState != State.WALK_RIGHT) ChangeState(State.WALK_RIGHT);
            else if (inputDir < 0 && playerState != State.WALK_LEFT) ChangeState(State.WALK_LEFT);
            else if (inputDir == 0)
            {
                if (playerState == State.WALK_RIGHT) { ChangeState(State.IDLE_RIGHT); }
                else if (playerState == State.WALK_LEFT) { ChangeState(State.IDLE_LEFT); }
               // else if (playerState == State.FALLING || playerState == State.JUMPING) { ChangeState(State.IDLE_RIGHT); }
            }
        }
        if(playerState == State.JUMPING)
        {
            if(rb.velocity.y <= 0)
            {
                ChangeState(State.FALLING);
          
            }
        }   
    }

    public void AttemptJumpOrDash()
    {
        if (target == null && !isGrounded) return;

        if (target == null && isGrounded)
        {
            Jump();
        }
        else if(canDash)
        {
            GrabHook();
            WaitTillHook();
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        ChangeState(State.JUMPING);
    }

    public void WaitTillHook()
    {
        hook.GetComponent<SpriteRenderer>().enabled = false;
        if (target.position.x > transform.position.x && playerState != State.SHOOT_RIGHT) { ChangeState(State.SHOOT_RIGHT); }
        else if (playerState != State.SHOOT_LEFT) { ChangeState(State.SHOOT_LEFT); }


        rb.gravityScale = 0f;

        hook.ShootHookTo(target);
        rb.velocity = Vector2.zero;
        hook.canHit = true;
    }

    public void Dash()
    {
 
        TimeAction?.Invoke(false);
        //TurnTime(false);
        rb.gravityScale = 0f;
        rb.drag = 8f;

        StartCoroutine(StartDashing());

        Debug.DrawLine(this.transform.position, (Vector2)this.transform.position + (dashDirection * dashSpeed), Color.green, 1f);
    }
    IEnumerator StartDashing()
    {
        
        canDash = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashStopTime);
        dashStrat.TryDefaultDash(this.transform, dashDirection);

       // playerAnimation.DashAnim(rb.velocity);
        StartCoroutine(FinishDashing());
    }

    IEnumerator FinishDashing()
    {
       
        yield return new WaitForSeconds(dashTime);
        canDash = true;
        if (rb.velocity.y > 0f) ChangeState(State.JUMPING);
        else ChangeState(State.FALLING);
        
        rb.drag = 2f;
        rb.gravityScale = 1f;
        playerAnimation.EndDashAnim();
    }

    public void GrabHook()
    {
        hook.transform.parent = this.transform;
        hook.transform.position = this.transform.position;
        hook.canHit = false;
        hook.SetTrailActive(false);
        hook.Catch();
    }

    //Move this out of the class. Or move dashing out of the class.
    public void HandleMovementInput(float horizontalFloat)
    {
        isRight = horizontalFloat > 0 ? true : false;
        if (playerState == State.SHOOT_RIGHT || playerState == State.SHOOT_LEFT) return;
        if (isGrounded)
        {
            // transform.Translate(Vector2.right * horizontalFloat * airMoveSpeed * Time.fixedDeltaTime, Space.Self);
            rb.velocity = new Vector2(horizontalFloat * moveSpeed, rb.velocity.y);
        }

        else if (playerState == State.FALLING || playerState == State.JUMPING)
        {
            //Player is falling without moving

            if (horizontalFloat != 0)
            {
                rb.velocity = new Vector2(horizontalFloat * airMoveSpeed, rb.velocity.y);
            }

            //Player is falling while moving

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

    public bool GetIsGrounded() { return isGrounded; }
    public State GetPlayerState() { return playerState; }

    private Vector2 GetDashDirection(Transform target)
    {
        Vector2 dashDirection = (target.transform.position - this.transform.position).normalized;

        Debug.Log(dashDirection);
        DrawnAngle(target, dashDirection);
        return dashDirection;
    }


    private void CheckForGround()
    {
        isGrounded = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, checkGroundDistance, whatIsGround);
        Debug.DrawRay(this.transform.position, Vector2.down * checkGroundDistance, Color.yellow);
        if (!isGrounded && playerState != State.SHOOT_LEFT && playerState != State.SHOOT_RIGHT)
        {
            if (playerState == State.FALLING) return;
            if (playerState == State.JUMPING) return;

          
            if(rb.velocity.y < 0) ChangeState(State.FALLING);
        }                   
        else if (isGrounded)
        {
            if (playerState == State.FALLING)
            {
                

                ChangeState(State.IDLE_RIGHT);
            }
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
        else
        {
            TimeAction?.Invoke(false);
            //TurnTime(false);
            return null;
        }

    }

    private Transform FindClosestTarget(RaycastHit2D[] targets)
    {
        Transform closest = targets[0].transform;
        foreach (RaycastHit2D hit in targets)
        {
            if ((hit.transform.position - this.transform.position).sqrMagnitude < (closest.transform.position - this.transform.position).sqrMagnitude)
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
        playerAnimation.SetPlayerAnimation(state);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Dashable>() && target == collision.GetComponent<Transform>())
        {
            if(playerState == State.SHOOT_LEFT || playerState == State.SHOOT_RIGHT)
            {
                
                dashStrat.TryDash(this.transform, dashDirection);
                GrabHook();
            }
            

        }
        if (collision.tag.Equals("DeathZone"))
        {
            GrabHook();
            Die();

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Dashable>() && target == collision.GetComponent<Transform>())
        {
            GrabHook();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Dashable>() && target == collision.GetComponent<Transform>())
        {
            GrabHook();
        }
    }

    private void DrawnAngle(Transform target, Vector2 dir)
    {

        Debug.DrawLine(this.transform.position, ((Vector2)this.transform.position + dir * 5f), Color.yellow);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3.down) * checkGroundDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, dashRadius);
        Gizmos.color = Color.green;
        Gizmos.color = Color.yellow;
        Gizmos.color = Color.cyan;
        if(target != null)
        Gizmos.DrawLine(this.transform.position, target.transform.position);
    }
}
