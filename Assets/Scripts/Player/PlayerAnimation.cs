using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DashEnums;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator anim;
    [SerializeField] private Transform graphics;

    // Player animations
    const string WALK_LEFT = "pLWalk";
    const string WALK_RIGHT = "pRWalk";
    const string IDLE_LEFT = "pLIdle";
    const string IDLE_RIGHT = "pRIdle";
    const string SHOOT_LEFT = "pLShoot";
    const string SHOOT_RIGHT = "pRShoot";
    const string JUMP = "pJump";
    const string FALL = "pFall";


    /* [Public] HANDLE PLAYER ANIMATIONS
     * 
     * Set player animation state from any script by using PlayerState enum
     */
    public void SetPlayerAnimation(State _playerState)
    {
        switch (_playerState)
        {
            case State.IDLE_LEFT:
                anim.Play(IDLE_LEFT);
                break;
            case State.IDLE_RIGHT:
                anim.Play(IDLE_RIGHT);
                break;
            case State.WALK_LEFT:
                anim.Play(WALK_LEFT);
                break;
            case State.WALK_RIGHT:
                anim.Play(WALK_RIGHT);
                break;
            case State.SHOOT_LEFT:
                anim.Play(SHOOT_LEFT);
                break;
            case State.SHOOT_RIGHT:
                anim.Play(SHOOT_RIGHT);
                break;
            case State.JUMPING:
                anim.Play(JUMP);
                break;
            case State.FALLING:
                anim.Play(FALL);
                break;
            default: break;
        }
    }

    // TODO - update this to rotate the sprite to face the node at dash start
    public void DashAnim(Vector2 dashDir)
    {
        if (dashDir.x >= transform.position.x) SetPlayerAnimation(State.SHOOT_RIGHT);
        else SetPlayerAnimation(State.SHOOT_LEFT);
        //this.anim.SetTrigger("Dash");
        //float angle = Mathf.Atan2(dashDir.y, dashDir.x) * Mathf.Rad2Deg;
        //graphics.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // TODO - update this to reset sprite rotation when the player reaches the node
    public void EndDashAnim()
    {
        SetPlayerAnimation(State.FALLING);
        //this.anim.SetTrigger("EndDash");
        //graphics.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }
}
