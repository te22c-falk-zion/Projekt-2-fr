using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ComboDisplay : MonoBehaviour
{

    [SerializeField]
    TMP_Text comboText;
    [SerializeField]
    walkController player;
    void Start()
    {
        comboText.text = "";
    }


    void Update()
    {
        if (player.hasCombo == true)
        {
            comboText.text = player.combo + "\nCombo!!";
        }
        
    }
}
