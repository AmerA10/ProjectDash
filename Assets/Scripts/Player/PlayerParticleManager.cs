using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DashEnums;

public class PlayerParticleManager : MonoBehaviour
{
    //[SerializeField]
    //PlayerController playerController;
    [SerializeField]
    Transform follow;
    ParticleSystem particleSystem;
    bool following;
    float timer;

    public float followTime = 0.3f;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        following = false;
        timer = 0f;
    }
    private void OnEnable()
    {
        EventManager.OnPreDash += EnableParticles;
    }

    private void OnDisable()
    {
        EventManager.OnPreDash -= EnableParticles;
    }

    private void Update()
    {
        if (following)
        {
            if(timer < followTime)
            {
                timer += Time.deltaTime;
                transform.position = follow.position;
            }
            else
            {
                timer = 0f;
                following = false;
            }
        }
    }

    void EnableParticles()
    {
        //State playerState = playerController.GetPlayerState();
        //float rotation = playerState == State.SHOOT_LEFT ? 0f : 180f;

        //transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        //transform.position = hookTransform.position;
        following = true;
        particleSystem.Play();
    }
}
