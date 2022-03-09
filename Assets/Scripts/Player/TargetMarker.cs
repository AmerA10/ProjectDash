using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TargetMarker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerController pc;
    Transform target;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        target = pc.GetTarget();
        if (target != null)
        {
            sr.enabled = true;
            this.transform.position = target.position;
        }
        else
        {
            sr.enabled = false;
        }
    }
}
