using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM_audio;
    public GameObject off;
    public int IS_on;
    // Start is called before the first frame update
    void Start()
    {
        IS_on = LoadDefaut();
        if (IS_on == 0)
        {
            BGM_audio.Play();
            off.SetActive(false);
        }
        else {
            BGM_audio.Stop();
            off.SetActive(true);
        }
    }

    public void Turn() {
        print("turn from " +IS_on);
        if (IS_on == 1)
        {
            IS_on = 0;
            BGM_audio.Play();
            SaveDefaut();
            off.SetActive(false);
        }
        else {
            IS_on = 1;
            BGM_audio.Stop();
            SaveDefaut();
            off.SetActive(true);
        }
    }

    public void SaveDefaut() {
        PlayerPrefs.SetInt("audio", IS_on);
    }

    public int LoadDefaut()
    {
        return PlayerPrefs.GetInt("audio");
    }
}
