using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager {
    public static string nextSceneName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void LoadScene(string sceneName) {
        nextSceneName = sceneName;
        SceneManager.LoadScene("Loading");
    }

    public static void LoadSceneWithOutLoading()
    {

    }

    public static void test() {
        MonoBehaviour.print("test");
    }
}
