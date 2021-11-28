using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DashEnums;

public class PlayerParticleManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        EventManager.OnPreDash += EnableParticles;
    }

    private void OnDisable()
    {
        EventManager.OnPreDash -= EnableParticles;
    }

    void EnableParticles()
    {
        State playerState = playerController.GetPlayerState();
        float rotation = playerState == State.SHOOT_LEFT ? 0f : 180f;

        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        particleSystem.Play();
        transform.position = playerController.transform.position;
    }
}
