using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardStarter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject hazard;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            if (hazard != null) 
            hazard.GetComponent<IHazard>().StartHazard();
            else
            {
                Debug.LogWarning("Hazard has not been set at :" + this);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hazard != null) 
        Gizmos.DrawLine(this.transform.position, hazard.transform.position);
    }
}
