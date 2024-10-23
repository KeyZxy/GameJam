using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScript : MonoBehaviour
{
    public AudioClip audioclip;
    AudioSource audiosource;
    void Start()
    {
        audioclip = GetComponent<AudioClip>();
        //audiosource = GetComponent<AudioSource>();
    }
    public void OnReturn()
    {
        //audiosource.clip = audioclip;
        //audiosource.Play();
        SceneManager.LoadScene("StartScene");
    }
}
