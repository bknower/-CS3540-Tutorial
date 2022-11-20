using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// simplified version of our game's player controller
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private Camera mainCamera;
    
    public float speed = 6f;
    public bool grounded;
    public float jumpHeight = 3f;
    public float gravity = -9.8f;
    
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerTransform = transform;
        
        
        //Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = (playerTransform.right * x) + (playerTransform.forward * z);
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        controller.Move(speed * Time.deltaTime * movement);

        //Gravity and Jumping
        if (!grounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            // constantly push slightly into the ground so character controller grounded works
            velocity.y = gravity * .01f;
        }
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight); 
        }
        
        controller.Move(velocity * Time.deltaTime);
        grounded = controller.isGrounded;
    }
}
