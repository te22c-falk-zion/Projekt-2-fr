
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class walkController : MonoBehaviour
{
    


    

    [Header("Camera")]
    Camera head;
    public Transform cameraHolder;
    Vector2 lookinput;
    float xRotation = 0;
    [SerializeField]
    Vector2 sensitivity = Vector2.one;
    [SerializeField]
    bool CameraDown = false;
    [SerializeField]
    bool hasloweredcamera = false;

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
    public bool hasCombo = false;
    public float speedMult = 1;
    float bulletReach = 200;


    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        head = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.transform.Translate(0,-0.4f,0);
        CameraDown = true;
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

        if(CameraDown && !hasloweredcamera)
        {
            print("Should be lowering camera.");
        }
        if(!CameraDown && hasloweredcamera)
        {
            print("Camera should go up.");
        }
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
            hasloweredcamera = false;
            StopSliding();
        }
        if(isSlideUp == true)
        {
            if(Input.GetButton("Slide"))
            {
                CameraDown = true;
                StartSliding();
                
            }
        }
    }
    void StartSliding()
    {
        if(isGrounded && slideCounter < timetoslide)
        {
            
            if (CameraDown && !hasloweredcamera)
            {
                Camera.main.transform.Translate(0,-0.3f,0);
                hasloweredcamera = true;
                CameraDown = false;
            }
            slideCounter += Time.deltaTime;
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
        CameraDown = false;
        isSlideUp = false;
        if(!CameraDown && hasloweredcamera)
        {
            Camera.main.transform.Translate(0,0.3f,0);
            hasloweredcamera = false;
        }
        
        slideCooldownCounter += Time.deltaTime;
        if(slideCooldownCounter > slideCooldown) 
        {
            isSlideUp = true;
            CameraDown = true;
            slideCounter = 0.0f;
            slideCooldownCounter = 0.0f;
            
        }
    }

    public void Run()
    {
        if (combo > 0)
        {
            hasCombo = true;
        }
        if (Input.GetButton("Run"))
        {Speed = runSpeed;}
        else if (!Input.GetButton("Run"))
        {Speed = walkSpeed;}
        if (Input.GetButton("Run") && hasCombo == true)
        {Speed = runSpeed * speedMult;}
        else if (!Input.GetButton("Run") && hasCombo == true)
        {Speed = walkSpeed * speedMult;}
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with: " + collision.gameObject.name);
        if(collision.gameObject.CompareTag("Goal"))
        {
            SceneManager.LoadScene(1);
        }
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
            print("I hit something.");
            TargetController target = hit.transform.GetComponent<TargetController>();
            if (target == null)
            {
                print("Im here rn");
                combo = 0;
                hasCombo = false;
            }
            if (target != null)
            {
                print("Hit target and found target.");
                target.SpeedBoost();
                target.DeleteMe();
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
