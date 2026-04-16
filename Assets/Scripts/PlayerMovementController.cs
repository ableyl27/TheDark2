using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Transform cameraTransform;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
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

         // AI
         /*
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
        }
        */
        }
        // AI

        //old
        /*
        if (moveDirection.magnitude > 0.1f)
        {
        transform.forward = moveDirection;
        }
        */
}
