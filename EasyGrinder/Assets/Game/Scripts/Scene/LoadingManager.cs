using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {
    public Slider loadingSlider;
    public Text loadingText;

    private AsyncOperation async = null;
    // Use this for initialization
    void Start () {
        StartCoroutine(LoadSceneAsync());
      
    }
	
	// Update is called once per frame
	void Update () {
        ProgressAdd();
    }

    void ProgressAdd() {
        if (loadingSlider.value < 1)
        {
            loadingSlider.value += 0.05f;
        }
        else {
            if (async!= null)
                async.allowSceneActivation = true;
        }
    }

    public void loadingSliderChange() {
        loadingText.text = "Loading..." + (int)(loadingSlider.value*100) + "%";
    }

    IEnumerator LoadSceneAsync() {
        yield return new WaitForEndOfFrame();
        //yield return null;
        async = SceneManager.LoadSceneAsync(GameSceneManager.nextSceneName);
        async.allowSceneActivation = false;

        //yield return async;

    }
}
