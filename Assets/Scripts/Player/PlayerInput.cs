using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerInput : MonoBehaviour
{
    private float horizontalInput;
    private PlayerController playerController;
    [SerializeField] private bool isInteracting = false;
    private bool dashInput;

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
        if(dashInput && !isInteracting)
        {

            playerController.AttemptJumpOrDash();
        }
    }

    private void FixedUpdate()
    {
        playerController.HandleMovementInput(horizontalInput);
    }

    public float GetPlayerInputDirection() { return horizontalInput; }
    public void SetInteracting(bool interacting)
    {
        this.isInteracting = interacting;
    }
}
