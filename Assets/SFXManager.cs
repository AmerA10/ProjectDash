using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DashEnums;

public class SFXManager : MonoBehaviour
{
    public List<AudioClip> Sounds;
    public AudioSource source;
    public AudioSource source2;

    public void PlayClip(State previousState, State playerState)
    {
        AudioClip clip;
        switch (playerState)
        {
            case State.WALK_LEFT:
            case State.WALK_RIGHT:
                // This is to handle the landing sound
                if (previousState.Equals(State.FALLING) && !playerState.Equals(State.DEAD))
                {
                    clip = Sounds[1];
                    source2.clip = clip;
                    source2.loop = false;
                    source2.pitch = 2.0f;
                    source.Play();
                }

                clip = Sounds[0];
                source.clip = clip;
                source.loop = true;
                source.pitch = 2.0f;
                source.Play();
                break;
            case State.JUMPING:
                HandleJumpSound(previousState);
                break;
            case State.SHOOT_LEFT:
            case State.SHOOT_RIGHT:
                clip = Sounds[2];
                source.clip = clip;
                source.loop = false;
                source.pitch = 2.0f;
                source.Play();
                break;
            case State.DEAD:
            case State.IDLE_LEFT:
            case State.IDLE_RIGHT:
            case State.FALLING:
                // This is to handle the landing sound
                if (previousState.Equals(State.FALLING) && !playerState.Equals(State.DEAD))
                {
                    clip = Sounds[1];
                    source2.clip = clip;
                    source2.loop = false;
                    source2.pitch = 2.0f;
                    source2.Play();
                }
                else
                {
                    source.Stop();
                }
                break;
            default:
                break;
        }
    }

    void HandleJumpSound(State prevState)
    {
        switch (prevState)
        {
            case State.IDLE_LEFT:
            case State.IDLE_RIGHT:
            case State.WALK_LEFT:
            case State.WALK_RIGHT:
                AudioClip clip = Sounds[1];
                source.clip = clip;
                source.loop = false;
                source.pitch = 2.5f;
                source.Play();
                break;
            default: break;
        }
    }
}
