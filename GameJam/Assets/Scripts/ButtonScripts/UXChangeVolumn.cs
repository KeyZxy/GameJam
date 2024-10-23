using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UXChangeVolumn : MonoBehaviour
{
    //µ÷ÒôÁ¿
    public AudioSource volumnAudioSource;
    public Slider volumnSlider;
    private void Start()
    {
        volumnSlider = GetComponent<Slider>();
        volumnAudioSource = GetComponent<AudioSource>();
    }
    public void OnDrag()
    {
        volumnAudioSource.volume = volumnSlider.value;
        PlayerPrefs.SetFloat("volumn", volumnSlider.value);
    }
}