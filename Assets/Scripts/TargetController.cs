using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    walkController controller;
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<walkController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpeedBoost()
    {
        controller.combo =+ 1;
        controller.speedMult = controller.combo/20 + 1;
        controller.walkSpeed *= controller.speedMult;
        controller.runSpeed *= controller.speedMult;
        
    }
    public void DeleteMe()
    {
        Destroy(this.gameObject);
    }
}
