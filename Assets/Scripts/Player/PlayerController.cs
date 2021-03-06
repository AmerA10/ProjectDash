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
    private SFXManager sfxManager;

    public Action OnDeath;

    private bool isRight = true;

    [Header("Acceleration On Ground")]
    [SerializeField] private float maxSpeed = 8f;
    [Tooltip("How long it takes for the player, to reach maximum speed")]
    [SerializeField] private float zeroToMaxTime = 1.0f;
    [Tooltip("How quickly the player decelerates, the higher the number, the more quickly the player stops moving")]
    [SerializeField] private float decelerationRate = 5f;

    private float accelRatePerSec;
    private float rightVelocity;

    [Header("Acceleration In Air")]
    [Tooltip("How long it takes for the player, to reach maximum speed in the air")]
    [SerializeField] private float airZeroToMaxTime = 1.0f;
    [Tooltip("How quickly the player decelerates in the air, the higher the number, the more quickly the player stops moving in the air")]
    [SerializeField] private float airDecelerationRate = 5f;
    private float airAccelRatePerSec;


    [Header("Falling")]
    [Tooltip("How quickly the player falls when the player y velocity is less than zero")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowFallMultiplier = 2f;
    [SerializeField] private float airLerp = 5f;
    [SerializeField] private float maximumFallSpeed = -30f;
    private Vector2 minimFallingSpeedVector;
    private bool isHoldingJump = false;

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
    [SerializeField] private float minimumHookDistance = 0.11f;
    [SerializeField] private float minDashRation = 0.8f;
    private float distanceFromTargetOnDash;
    private float dashRatio;
    
    private bool isHookCaught = false;
    private Vector2 dashDirection;

    [Header("Jump Stuff")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyeteTimeCounter;
    [SerializeField] private float jumpBufferLength = 0.1f;
    [SerializeField] private float jumpBufferCounter;

    public Action<bool> TimeAction;

    public void Die()
    {
        rb.velocity = Vector2.zero;
        canDash = true;
        OnDeath();
    }

    private void Awake()
    {
        Application.targetFrameRate = 200;
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerInput = GetComponent<PlayerInput>();
        sfxManager = GetComponent<SFXManager>();

        minimFallingSpeedVector = new Vector2(rb.velocity.x, maximumFallSpeed);
        ChangeState(State.IDLE_RIGHT);
        hook.OnHookHit += Dash;
        GrabHook();
        accelRatePerSec = maxSpeed / zeroToMaxTime;
        airAccelRatePerSec = maxSpeed / airZeroToMaxTime;
        rightVelocity = 0f;

    }

    void Update()
    {
        jumpBufferCounter = Mathf.Max(jumpBufferCounter - Time.deltaTime, -1f);
        CheckForGround();
        if (!isGrounded)
        {
            coyeteTimeCounter = Mathf.Max(coyeteTimeCounter - Time.deltaTime, -1f);
        }

        if ((playerState != State.SHOOT_LEFT && playerState != State.SHOOT_RIGHT))
            target = CheckForDashTarget();
        if (target != null && (playerState != State.SHOOT_LEFT && playerState != State.SHOOT_RIGHT))
        {

            canDash = true;
        }
        else
        {
            canDash = false;
        }
        AdjustPlayerState();

        if (target != null && canDash)
        {
            dashStrat = target.GetComponent<Dashable>();
            dashDirection = GetDashDirection(target);
            TimeAction?.Invoke(true);

        }

        if (Input.GetKeyDown(KeyCode.P)) Die();
    }
    //Putting this here might eliminate a race conidtion between pressing the space button the InputScript and jumping int his script
    private void LateUpdate()
    {
        if (isGrounded && jumpBufferCounter > 0)
        {
            Jump();
        }
        if (playerState == State.FALLING)
        {
            rb.velocity = Vector2.Max(rb.velocity, minimFallingSpeedVector);
        }
    }

    private void AdjustPlayerState()
    {
        if (playerState == State.SHOOT_LEFT || playerState == State.SHOOT_RIGHT || playerState == State.STUNNED) return;

        if (isGrounded && playerState != State.JUMPING)
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
        if (playerState == State.JUMPING)
        {
            if (rb.velocity.y <= 0)
            {
                ChangeState(State.FALLING);

            }
        }
    }

    public void AttemptJumpOrDash()
    {
        if (target == null && playerState == State.FALLING)
        {

            jumpBufferCounter = jumpBufferLength;

        }

        if ((target == null && isGrounded) || (target == null && coyeteTimeCounter > 0f && jumpBufferCounter > 0))
        {

            Jump();
        }
        else if (canDash)
        {

            GrabHook();
            StartCoroutine(WaitTillHook());
        }
    }

    public IEnumerator WaitTillHook()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;

        hook.GetComponent<SpriteRenderer>().enabled = true;
        if (target.position.x > transform.position.x && playerState != State.SHOOT_RIGHT)
        {
            isRight = true;
            ChangeState(State.SHOOT_RIGHT);
        }
        else if (playerState != State.SHOOT_LEFT)
        {
            isRight = false;
            ChangeState(State.SHOOT_LEFT);
        }

        yield return StartCoroutine(hook.ShootHookTo(target));
        distanceFromTargetOnDash = (target.position - this.transform.position).sqrMagnitude;
        Debug.Log("Dsitance from Target Dash" + distanceFromTargetOnDash + " Minumum Hook Distance squared" + (minimumHookDistance * minimumHookDistance));
        dashRatio = Mathf.Max(Mathf.Min((distanceFromTargetOnDash / (dashRadius * dashRadius)), 1f), minDashRation);
        EventManager.Instance.TriggerCameraEffect(CameraEffect.PRE_DASH);
        CheckMinimumHookDistance();

        hook.canHit = true;
    }

    private void CheckMinimumHookDistance()
    {
        if (target == null) return;

        if (distanceFromTargetOnDash < minimumHookDistance * minimumHookDistance)
        {
            GrabHook();
            dashStrat.TryDash(this.transform, dashDirection, dashRatio);
        }
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

        dashStrat.TryDefaultDash(this.transform, dashDirection, dashRatio);

        yield return new WaitForSeconds(dashTime);
        // playerAnimation.DashAnim(rb.velocity);
        canDash = true;
        FinishDashing();
    }

    private void FinishDashing()
    {
        rightVelocity = rb.velocity.x;
        if (rb.velocity.y > 0f) ChangeState(State.JUMPING);
        else ChangeState(State.FALLING);

        rb.drag = 1f;
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

    #region Movement
    public void HandleMovementInput(float horizontalFloat)
    {

        isRight = horizontalFloat > 0 ? true : horizontalFloat < 0 ? false : horizontalFloat == 0 ? isRight : isRight;
        if (playerState == State.SHOOT_RIGHT || playerState == State.SHOOT_LEFT || playerState == State.STUNNED) return;
        if (isGrounded)
        {
            if (horizontalFloat != 0)
            {
                // transform.Translate(Vector2.right * horizontalFloat * airMoveSpeed * Time.fixedDeltaTime, Space.Self);
                rightVelocity += horizontalFloat * accelRatePerSec * Time.deltaTime;
                rightVelocity = (horizontalFloat < 0) ? Mathf.Max(-maxSpeed, rightVelocity) : Mathf.Min(maxSpeed, rightVelocity);

                //rb.velocity = new Vector2(horizontalFloat * moveSpeed, rb.velocity.y);
            }
            else
            {
                rightVelocity -= (isRight ? decelerationRate : -decelerationRate) * Time.deltaTime;
                rightVelocity = (isRight ? Mathf.Max(0f, rightVelocity) : Mathf.Min(0f, rightVelocity));


            }
            rb.velocity = new Vector2(rightVelocity, rb.velocity.y);
        }

        else if (playerState == State.FALLING || playerState == State.JUMPING)
        {
            //Player is falling without moving

            if (horizontalFloat != 0)
            {

                rightVelocity += horizontalFloat * airAccelRatePerSec * Time.deltaTime;
                rightVelocity = (horizontalFloat < 0) ? Mathf.Max(-maxSpeed, rightVelocity) : Mathf.Min(maxSpeed, rightVelocity);

            }
            else
            {
                rightVelocity -= (isRight ? airDecelerationRate : -airDecelerationRate) * Time.deltaTime;
                rightVelocity = (isRight ? Mathf.Max(0f, rightVelocity) : Mathf.Min(0f, rightVelocity));

            }

            rb.velocity = new Vector2(rightVelocity, rb.velocity.y);

            if (rb.velocity.y <= 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !isHoldingJump)
            {

                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowFallMultiplier - 1) * Time.deltaTime;
            }
        }
        else
        {
            return;
        }
    }

    public void Jump()
    {
        jumpBufferCounter = 0f;
        coyeteTimeCounter = 0f;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isHoldingJump = true;
        ChangeState(State.JUMPING);
    }

    public IEnumerator Stun(float duration)
    {
        State previousState = this.playerState;
        rb.velocity = Vector2.zero;
        this.ChangeState(State.STUNNED);
        yield return StartCoroutine(StunPlayerForDuration(duration));
        Debug.Log("done waiting");
        this.ChangeState(previousState);
    }
    private IEnumerator StunPlayerForDuration(float duration)
    {
        Debug.Log("WAITING");
        yield return new WaitForSeconds(duration);
    }

    public void Fall()
    {
        if (playerState == State.JUMPING || playerState == State.FALLING)
        {
            //Do the fall stuff here
            coyeteTimeCounter = 0f;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.fixedDeltaTime;
        }
    }
    public void SetIsHoldingJump(bool isHoldingJump)
    {
        this.isHoldingJump = isHoldingJump;
    }
    public Transform GetTarget()
    {
        return target;
    }
    #endregion
    public bool GetIsGrounded() { return isGrounded; }
    public State GetPlayerState() { return playerState; }

    private Vector2 GetDashDirection(Transform target)
    {
        Vector2 dashDirection = (target.transform.position - this.transform.position).normalized;

        DrawnAngle(target, dashDirection);
        return dashDirection;
    }


    private void CheckForGround()
    {
        // isGrounded = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, checkGroundDistance, whatIsGround);
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, checkGroundDistance, whatIsGround);
        Debug.DrawRay(this.transform.position, Vector2.down * checkGroundDistance, Color.yellow);
        if (!isGrounded && playerState != State.SHOOT_LEFT && playerState != State.SHOOT_RIGHT)
        {

            if (playerState == State.JUMPING) return;

            if (rb.velocity.y < 0) ChangeState(State.FALLING);

        }
        else if (isGrounded)
        {
            if (playerState == State.FALLING)
            {
                coyeteTimeCounter = coyoteTime;
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
            canDash = false;
            return null;
        }

    }

    private Transform FindClosestTarget(RaycastHit2D[] targets)
    {
        Transform closest = targets[0].transform;
        foreach (RaycastHit2D hit in targets)
        {

            if ((hit.transform.position - this.transform.position).x >= 0 && isRight)
            {
                if ((closest.position - this.transform.position).x >= 0)
                {
                    if ((hit.transform.position - this.transform.position).sqrMagnitude < (closest.transform.position - this.transform.position).sqrMagnitude)
                    {
                        closest = hit.transform;
                    }
                }
                else
                {
                    closest = hit.transform;
                }

            }
            if ((hit.transform.position - this.transform.position).x < 0 && !isRight)
            {
                if ((closest.position - this.transform.position).x < 0)
                {
                    if ((hit.transform.position - this.transform.position).sqrMagnitude < (closest.transform.position - this.transform.position).sqrMagnitude)
                    {
                        closest = hit.transform;
                    }
                }
                else
                {
                    closest = hit.transform;
                }

            }

        }
        Debug.Log("Closest is: " + (closest.position - this.transform.position).x);
        return closest;
    }
    private void ChangeState(State state)
    {

        if (playerState == state) return;
        sfxManager.PlayClip(playerState, state);
        playerState = state;
        playerAnimation.SetPlayerAnimation(state);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Dashable>() && target == collision.GetComponent<Transform>())
        {
            if (playerState == State.SHOOT_LEFT || playerState == State.SHOOT_RIGHT)
            {
                GrabHook();
                dashStrat.TryDash(this.transform, dashDirection, dashRatio);

            }


        }
        if (collision.tag.Equals("DeathZone"))
        {
            GrabHook();
            Die();

        }
    }


    private void DrawnAngle(Transform target, Vector2 dir)
    {

        Debug.DrawLine(this.transform.position, ((Vector2)this.transform.position + dir * 5f), Color.yellow);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckTransform.position, checkGroundDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, dashRadius);
        Gizmos.color = Color.green;
        Gizmos.color = Color.yellow;
        Gizmos.color = Color.cyan;
        if (target != null)
            Gizmos.DrawLine(this.transform.position, target.transform.position);
    }
}
