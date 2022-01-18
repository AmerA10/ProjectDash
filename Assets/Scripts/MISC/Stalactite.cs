using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    enum State { FALLING, WAITING, RESETTING };

    BoxCollider2D collider;
    Rigidbody2D rigidbody;
    Vector3 origin;
    SpriteRenderer sprite;

    float timer;
    State state = State.WAITING;


    public float interval;


    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
        origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        timer = 0f;
    }

    private void Update()
    {
        if (!state.Equals(State.FALLING))
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                if (state.Equals(State.WAITING)) TriggerFall();
                else if (state.Equals(State.RESETTING)) Reset();
            }

        }

    }

    private void TriggerFall()
    {
        state = State.FALLING;
        rigidbody.isKinematic = false;
    }

    void DisableStalactite()
    {
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector2.zero;
        state = State.RESETTING;
        transform.position = origin;
        collider.enabled = false;
        sprite.enabled = false;
        timer = 0f;
    }

    private void Reset()
    {
        state = State.WAITING;
        timer = 0f;
        transform.position = origin;
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector2.zero;
        sprite.enabled = true;
        collider.enabled = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state.Equals(State.FALLING))
        {
            DisableStalactite();
        }
    }
}
