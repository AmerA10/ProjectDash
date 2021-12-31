using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerInput : MonoBehaviour
{
    private float horizontalInput;
    private PlayerController playerController;
    private bool dashInput;
    private IInteractable interactable;

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
            if (interactable != null && interactable.IsInteractable()) interactable.HandleInteraction();
            else
            {
                playerController.AttemptJumpOrDash();
            }
            
        }
    }

    private void FixedUpdate()
    {
        playerController.HandleMovementInput(horizontalInput);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IInteractable>(out interactable))
        {
            Debug.Log("Got interactbale");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<IInteractable>() != null && collision.GetComponent<IInteractable>() == interactable)
        {
            interactable = null;
        }
    }

    public float GetPlayerInputDirection() { return horizontalInput; }

}
