
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
public class walkController : MonoBehaviour
{
    


    

    [Header("Camera")]
    Camera head;
    Vector2 lookinput;
    float xRotation = 0;
    [SerializeField]
    Vector2 sensitivity = Vector2.one;

    [Header("Movement")]
    [SerializeField]
    float Speed = 7;
    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;
    [SerializeField]
    private bool isGrounded;
    Vector2 moveInput;

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
    public float combo;
    public float speedMult = 1;
    float bulletReach = 20;


    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        head = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CamMove();


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
    void OnLook(InputValue value)
    {
        lookinput = value.Get<Vector2>();
    }
    void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    public void Run()
    {
        if (Input.GetButton("Run"))
        {Speed = runSpeed;}
        else if (!Input.GetButton("Run"))
        {Speed = walkSpeed;}
    }

        void OnFire(InputValue value)
    {
        RaycastHit hit;
        if(Physics.Raycast(
        head.transform.position,
         head.transform.forward,
         out hit,
         bulletReach)
         )
         {
            TargetController target = hit.transform.GetComponent<TargetController>();
            if (target != null)
            {
                target.SpeedBoost();
                target.DeleteMe();
            }
            if (target == null)
            {
                combo = 0;
            }
         }

    }

    void SetIsGrounded(bool state)
    {
        isGrounded = state;

    }

    void CamMove()
    {
        xRotation += -lookinput.y * sensitivity.y;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
    
        head.transform.localEulerAngles = new(
        xRotation, 0, 0
        );

        transform.Rotate(Vector3.up, lookinput.x * sensitivity.x);
    }

    
}
