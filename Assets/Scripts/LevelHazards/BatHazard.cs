using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatHazard : MonoBehaviour, IHazard, IReset
{

    [SerializeField] float knockbackForce = 10f;
    [SerializeField] float flyingSpeed = 10f;
    [SerializeField] bool isRight = false;
    Vector3 startingLocation;
    bool isFlying = false;
    Rigidbody2D rb;
    int dir;

    Animator anim;

    // Start is called before the first frame update
    
    void Awake()
    {
        startingLocation = this.transform.position;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("Flying", false);
        dir = (isRight) ? 1 : -1;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFlying)
        {
            Debug.Log("Bat flying at speed of: " + flyingSpeed * dir);
            rb.velocity = new Vector2(flyingSpeed * dir, 5f * Mathf.Sin(Time.time * 5f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag.Equals("Player"))
        {
           
            Knockback(collision.transform);
        }
    }

    private void Knockback(Transform player) {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Debug.Log("Applying knockback of" + knockbackForce * dir);
        playerRb.velocity = Vector2.zero;
        player.position = new Vector3(player.position.x + (knockbackForce * dir), player.position.y, player.position.z);
        GetComponent<CircleCollider2D>().enabled = false;

    
    }


    public void HandleReset()
    {
        this.transform.position = startingLocation;
        anim.SetBool("Flying", false);
        isFlying = false;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    public bool isResettable()
    {
        return true;
    }

    public void StartHazard()
    {
        isFlying = true;
        anim.SetBool("Flying", true);
    }

    public void StopHazard()
    {
        HandleReset();
    }
}
