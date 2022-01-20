using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<Transform> points = new List<Transform>();
    Rigidbody2D rb;
    int target = 1;
    [SerializeField] float speed;
    [SerializeField] float distanceCheck;
    [SerializeField] private Vector3 dirToTarget;
    [SerializeField] private Transform targetT;
    [SerializeField] Rigidbody2D prb;

    private bool isForward = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        foreach(Transform child in this.transform)
        {
            points.Add(child);
          

        }
        foreach(Transform point in points)
        {
            point.transform.parent = null;
        }
        transform.position = points[0].transform.position;
        CalculateDirToTarget();
    }

    private void CalculateDirToTarget()
    {

        targetT = points[target];
        dirToTarget = (points[target].transform.position - this.transform.position).normalized;
    }


    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(this.transform.position, points[target].transform.position) < distanceCheck)
        {
            CalculateTarget();
            CalculateDirToTarget();
        }
    }

    private void FixedUpdate()
    {

        if (targetT != null) MoveToTarget();    

    }
    private void CalculateTarget()
    {
        if (isForward) GetNextForward();
        else
        {
            GetNextBack();
        }
    }

    private void MoveToTarget()
    {
        rb.MovePosition(dirToTarget * speed);
        transform.Translate(dirToTarget * speed);
        //rb.velocity = dirToTarget * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            collision.transform.GetComponent<Rigidbody2D>().velocity = rb.velocity;
            collision.transform.parent = this.transform;
            prb = collision.transform.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.transform.parent = null;
            prb = null;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
   
        if (collision.tag.Equals("Player"))
        {

            if (dirToTarget.x * prb.velocity.x < 0)
            {
                prb.transform.parent = null;

            }
            else
            {
                prb.transform.parent = this.transform;
            }
        }
    }

    void GetNextForward()
    {
        if (target + 1 >= points.Count)
        {
            isForward = false;
            target = points.Count - 2;
        }
        else
        {
            target++;
        }
    }
    void GetNextBack()
    {
        if (target - 1 < 0)
        {
            isForward = true;
            target = 1;
        }
        else
        {
            target--;
        }
    }
}
