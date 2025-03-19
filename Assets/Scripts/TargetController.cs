using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    walkController controller;
    void Start()
    {
        controller = GetComponent<walkController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpeedBoost()
    {
        controller.combo += 1;
        controller.speedMult = controller.combo/10;
        print("Not yet okay?");

        
    }
    public void DeleteMe()
    {
        Destroy(this.gameObject);
    }
}
