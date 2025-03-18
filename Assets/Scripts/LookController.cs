using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class lookController : MonoBehaviour
{
    Camera head;
    Vector2 lookinput;
    float xRotation = 0;

    

    [SerializeField]
    Vector2 sensitivity = Vector2.one;

    void Start()
    {
        head = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }


    void Update()
    {
        xRotation += -lookinput.y * sensitivity.y;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
    
        head.transform.localEulerAngles = new(
        xRotation, 0, 0
        );

        transform.Rotate(Vector3.up, lookinput.x * sensitivity.x);
        // head.transform.Rotate(Vector3.right, -lookinput.y * sensitivity.y);

    }

    void OnLook(InputValue value)
    {
        lookinput = value.Get<Vector2>();
    }

    void OnUse(InputValue value)
    {
        RaycastHit hit;
        if(Physics.Raycast(
        head.transform.position,
         head.transform.forward,
         out hit,
         1)
         )
         {
            ButtonController button = hit.transform.GetComponent<ButtonController>();
            if (button != null)
            {
                button.Press();
            }
         }

    }
}
