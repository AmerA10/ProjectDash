using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerInput : MonoBehaviour
{
    private float horizontalInput;
    private PlayerController playerController;
  
    private bool dashInput;
    private bool isGrounded;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("You pressed l");
        }
        dashInput = Input.GetKeyDown(KeyCode.Space);
        if(dashInput)
        {

            playerController.AttemptJumpOrDash();
        }
    }

    private void FixedUpdate()
    {
        playerController.HandleMovementInput(horizontalInput);
    }
}
