using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    private bool isStopping;
    void Start()
    {
        isStopping = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isStopping)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Time.timeScale = 1;
                isStopping = false;

            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isStopping) return;
            isStopping = true;
            Time.timeScale = 0;
        }


    }
}