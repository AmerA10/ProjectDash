using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDash : Dashable
{
    // Start is called before the first frame update
    [SerializeField] public GameObject interactable;

    private void Awake()
    {
         defaultDash = new DSDefault();
        dashType = new DSTrigger(interactable.GetComponent<IInteractable>());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrawGizmosSelected()
    {
        if(interactable != null)
        {
            Gizmos.DrawLine(this.transform.position, interactable.transform.position);
        }
        
    }
}
