
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
public class walkController : MonoBehaviour
{
    


    

    [Header("Camera")]
    Camera head;
    Camera cam;
    Vector2 lookinput;
    float xRotation = 0;
    [SerializeField]
    Vector2 sensitivity = Vector2.one;

    [Header("Movement")]
    [SerializeField]
    public float Speed = 5;
    [SerializeField]
    public float walkSpeed = 5;
    [SerializeField]
    public float runSpeed = 10;
    [SerializeField]
    public float slideSpeed = 13;
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
    [SerializeField]
    float slideMult = 1.20f;
    float slideCounter = 0.0f;
    float timetoslide = 1.0F;
    float slideCooldownCounter = 0.0f;
    float slideCooldown = 0.7f;
    bool isSlideUp = true;
    private bool isSliding;


    [Header("Boosts")]
    public float combo;
    public float comboMult;
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
        Slide();

        SetIsGrounded(bottomCollider.IsColliding);
        
    }

    void OnJump(InputValue value)
    {
        print("Jump around!");
        jumpStart = true;

        if(jumpStart)
        {
            jumpStart = false;
            isSliding = false;

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
    void Slide()
    {   
        if(isSlideUp == false)
        {
            StopSliding();
        }
        if(isSlideUp == true)
        {
            if(Input.GetButton("Slide"))
            {
                StartSliding();
            }
        }
    }
    void StartSliding()
    {
        Vector3 headTransform = Camera.main.transform.position;

        if(isGrounded && slideCounter < timetoslide)
        {
            slideCounter += Time.deltaTime;
            headTransform.y = 0.25F;
            slideSpeed = Speed * slideMult;
            Speed = slideSpeed;
        }
        if(slideCounter > timetoslide)
        {
            StopSliding();
        }
    }

    void StopSliding()
    {
        isSlideUp = false;
        Vector3 headTransform = Camera.main.transform.position;
        headTransform.y = 0.5f;
        slideCooldownCounter += Time.deltaTime;
        if(slideCooldownCounter > slideCooldown) 
        {
            isSlideUp = true;
            slideCounter = 0.0f;
            slideCooldownCounter = 0.0f;
            
        }

    }

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
                // target.DeleteMe();
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
        if (!isGrounded && isSliding) StopSliding();
    }
    void SetIsSliding(bool state)
    {
        isSliding = state;
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
