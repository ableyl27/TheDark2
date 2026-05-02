using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 200f;
    public Transform cameraTransform;

    private CharacterController controller;
    private float xRotation = 0f;

    private Vector3 knockbackVelocity;
    private float knockbackDuration;

    
    public float gravity = -9.81f;
    private Vector3 velocity; 

    public void ApplyKnockback(Vector3 direction, float force, float duration)
    {
        knockbackVelocity = direction * force;
        knockbackDuration = duration;
    }
 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
      
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveZ + right * moveX;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        
        if (controller.isGrounded && velocity.y < 0)
        {
           
            velocity.y = -2f; 
        }

       
        velocity.y += gravity * Time.deltaTime;

        
        controller.Move(velocity * Time.deltaTime);

       
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (knockbackDuration > 0)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);
            knockbackDuration -= Time.deltaTime;
            return;
        }
    }
}