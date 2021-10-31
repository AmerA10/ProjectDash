using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector2 aimPosition;
    [SerializeField] private LayerMask whatIsShootable;
    [SerializeField] private float shootingDistance = 100f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      aimPosition = GetMousePosition() - (Vector2)this.transform.position;
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot(aimPosition);
        }

    }

    private void Shoot(Vector2 direciton)
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(this.transform.position, direciton, shootingDistance, whatIsShootable);
        Debug.DrawRay(this.transform.position, direciton, Color.yellow, 1f);
        if (hit)
        {
            if(hit.transform.GetComponent<IShootable>() != null)
            {
                hit.transform.GetComponent<IShootable>().OnShot();
            }
            Debug.Log("hit something");
        }


    }

    private Vector2 GetMousePosition()
    {
       return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

/*    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(GetMousePosition(), 2f);
    }*/
}
