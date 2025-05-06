using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // [SerializeField] TextMeshProUGUI textTimer;
    public TMP_Text textTime;
    public TMP_Text textCombo;
    private walkController controller;
    float floatTimer;
    int timer = 0;
    float highestCombo;
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<walkController>();
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

        textTime = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TMP_Text>();
        if(SceneManager.GetActiveScene().name == "MainGame")
        {
            floatTimer += Time.deltaTime;
            timer = (int)floatTimer;
            textTime.text = timer.ToString();
        }
        if(SceneManager.GetActiveScene().name == "WinScene")
        {
            textTime.text = "Time: " + timer;
        }               
        if (controller.combo > highestCombo)
        {
            highestCombo = controller.combo;
        }
        textCombo = GameObject.FindGameObjectWithTag("ComboText").GetComponent<TMP_Text>();
        textCombo.text = "Highest Combo: " + highestCombo;
    }
}
