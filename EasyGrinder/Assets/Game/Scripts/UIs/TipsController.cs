using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsController : MonoBehaviour
{
    private static TipsController Instance;
    public CanvasGroup canvas;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Show(string txt) {

        Instance.canvas.alpha = 1;
        Instance.text.text = txt;
    }

    public static void hide() {
        Instance.canvas.alpha = 0;
        Instance.text.text = "";
    }
}
