// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.UI;

// public class lookController : MonoBehaviour
// {
//     Camera head;
//     Vector2 lookinput;
//     float xRotation = 0;

    

//     [SerializeField]
//     Vector2 sensitivity = Vector2.one;

//     void Start()
//     {
//         head = GetComponentInChildren<Camera>();
//         Cursor.lockState = CursorLockMode.Locked;
        
//     }


//     void Update()
//     {
//         CamMove();
//         // head.transform.Rotate(Vector3.right, -lookinput.y * sensitivity.y);

//     }

//     void CamMove()
//     {
//         xRotation += -lookinput.y * sensitivity.y;
//         xRotation = Mathf.Clamp(xRotation, -90, 90);
    
//         head.transform.localEulerAngles = new(
//         xRotation, 0, 0
//         );

//         transform.Rotate(Vector3.up, lookinput.x * sensitivity.x);
//     }

//     void OnLook(InputValue value)
//     {
//         lookinput = value.Get<Vector2>();
//     }

//     void OnFire(InputValue value)
//     {
//         RaycastHit hit;
//         if(Physics.Raycast(
//         head.transform.position,
//          head.transform.forward,
//          out hit,
//          10)
//          )
//          {
//             TargetController target = hit.transform.GetComponent<TargetController>();
//             if (target != null)
//             {
//                 target.SpeedBoost();
//                 target.DeleteMe();
//             }
//          }

//     }
// }
