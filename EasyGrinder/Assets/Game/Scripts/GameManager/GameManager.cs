using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager ui_manager;
    // Start is called before the first frame update
    void Start()
    {
        SetResolution();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetResolution()
    {
        float heightScal = 9.0f;
        float widthScal = 16.0f;
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;
        int width = Screen.width;
        int height = Screen.height;
        if (((widthScal * height) / heightScal) > screenWidth)
        {
            int h = (int)((heightScal * screenWidth) / widthScal);
            int w = (int)((widthScal * h) / heightScal);
            Screen.SetResolution(w, h, true);
        }
        else
        {
            int w = (int)((widthScal * screenHeight) / heightScal);
            int h = (int)((heightScal * screenWidth) / widthScal);
            Screen.SetResolution(w, h, true);
        }
    }
}
