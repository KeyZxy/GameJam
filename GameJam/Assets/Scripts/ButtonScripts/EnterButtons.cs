using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterButtons : MonoBehaviour
{
    public AudioClip audioclip;
    AudioSource audiosource;
    void Start()
    {
        audioclip = GetComponent<AudioClip>();   
        audiosource = GetComponent<AudioSource>();
    }
    public void OnStartButton()
    {
        //audiosource.clip = audioclip;
        //audiosource.Play(); //播放音效
        StartCoroutine(StartGame());
    }
    public void OnSettingButton()
    {
        audiosource.clip = audioclip;
        audiosource.Play();
        SceneManager.LoadScene("Settings");
    }
    IEnumerator StartGame()
    {
        //按钮闪烁
        GameObject btn = GameObject.Find("Button");
        for (int i = 0; i < 6; i++)
        {
            btn.SetActive(!btn.activeInHierarchy);
            yield return new WaitForSeconds(0.2f);
        }
        SceneManager.LoadScene("GameScene1");   //进入游戏界面
    }
}
