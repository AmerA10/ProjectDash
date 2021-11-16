using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DashEnums
{
    public enum PlayerState
    {
        IDLE_LEFT,
        IDLE_RIGHT,
        WALK_LEFT,
        WALK_RIGHT,
        SHOOT_LEFT,
        SHOOT_RIGHT,
        JUMP,
        FALL
    }

    public enum State
    {
        STANDING, MOVING, DASHING, FALLING, DEAD, WAITING, JUMPING,
        IDLE_LEFT,
        IDLE_RIGHT,
        WALK_LEFT,
        WALK_RIGHT,
        SHOOT_LEFT,
        SHOOT_RIGHT,
    }
}
