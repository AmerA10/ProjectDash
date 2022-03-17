using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DashEnums
{
    public enum State
    {
        FALLING, 
        DEAD, 
        JUMPING,
        IDLE_LEFT,
        IDLE_RIGHT,
        WALK_LEFT,
        WALK_RIGHT,
        SHOOT_LEFT,
        SHOOT_RIGHT,
        STUNNED,

    }
    public enum CameraEffect
    {
        NONE,
        PRE_DASH,
        DASH, 
        DEATH, 
        TRANSITION, 
    }
}
