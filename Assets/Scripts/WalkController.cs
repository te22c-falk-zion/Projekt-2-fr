
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
public class walkController : MonoBehaviour
{
    


    Vector2 moveInput;

    [Header("Movement")]
    [SerializeField]
    float Speed = 7;
    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;
    private bool isGrounded;

    [Header("Checkers")]
    public CollisionDetectorRaycast bottomCollider;
    public CollisionDetectorRaycast leftCollider;
    public CollisionDetectorRaycast rightCollider;


    [Header("Jumping")]
    [SerializeField]
    float jumpforce = 100;
    float velocityY = 0;
    bool jumpStart = false;

    [Header("Sliding")]
    

    [Header("Boosts")]
    int combo;


    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {


        velocityY += Physics.gravity.y * Time.deltaTime;

        Vector3 movement =
        transform.right * moveInput.x
        + transform.forward * moveInput.y;
        
        movement.y = velocityY;
        
  
        controller.Move(Time.deltaTime * Speed * movement);
        
    }

    void FixedUpdate()
    {
        Run();

        SetIsGrounded(bottomCollider.IsColliding);
        
    }

    void OnJump(InputValue value)
    {
        print("Jump around!");
        jumpStart = true;

        if(jumpStart)
        {
            jumpStart = false;

            if(!isGrounded) return;
            print("I made it");

            velocityY = jumpforce;
        }


    }
    void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    public void Run()
    {
        if (Input.GetButton("Run"))
        {Speed = runSpeed;}
        else if (!Input.GetButton("Run"))
        {Speed = walkSpeed;}
    }

    void SetIsGrounded(bool state)
    {
        isGrounded = state;

    }
}
