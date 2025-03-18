using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class walkController : MonoBehaviour
{
    


    Vector2 moveInput;

    public Rigidbody rb;

    [Header("Movement")]
    [SerializeField]
    float Speed = 7;
    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;

    [Header("Checkers")]
    public CollisionDetectorRaycast bottomCollider;
    public CollisionDetectorRaycast leftCollider;
    public CollisionDetectorRaycast rightCollider;


    [Header("Jumping")]
    [SerializeField]
    float jumpforce = 100;
    float velocityY = 0;

    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        velocityY += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded)
        {
            velocityY = 0;
        }

        Vector3 movement =
        transform.right * moveInput.x
        + transform.forward * moveInput.y;
        
        movement.y = velocityY;

        Run();
        
  
        controller.Move(Time.deltaTime * Speed * movement);
        

        
        
    }

    void OnJump(InputValue value)
    {
        print("Jump around!");
        velocityY = jumpforce;
    }
    void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    public void Run()
    {
        if (Input.GetButton("Run"))
        {Speed = runSpeed;}
        else if (!Input.GetButton("Run"))
        {Speed = walkSpeed;}
    }
}
