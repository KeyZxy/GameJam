using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UXChangeIntensity : MonoBehaviour
{
    //������
    public Slider intensitySlider;
    private void Start()
    {
        intensitySlider.value = Screen.brightness;
    }
    public void OnDrag()
    {
        Screen.brightness = intensitySlider.value;
        PlayerPrefs.SetFloat("brightness", intensitySlider.value);
    }
}
