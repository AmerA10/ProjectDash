using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private  Animator anim;
   [SerializeField] private  Transform graphics;

    private void Awake()
    {

    }
    void Start()
    {
        
    }

    public void DashAnim (Vector2 dashDir)
    {
        this.anim.SetTrigger("Dash");

        graphics.rotation = Quaternion.LookRotation(Vector3.forward, dashDir) *  Quaternion.Euler(0,0, 90) ;
    }
    public void EndDashAnim()
    {
        this.anim.SetTrigger("EndDash");
        graphics.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));


    }

    // Update is called once per frame
    void Update() 
    {
       
    }
}
