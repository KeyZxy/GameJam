using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UXChangeResolution : MonoBehaviour
{
    //µ÷·Ö±æÂÊ
    private Resolution[] resolution;
    public Dropdown dropdown;
    
    void Start()
    {
        resolution = Screen.resolutions;
        dropdown.ClearOptions();
        for (int i = 0; i < resolution.Length; i++)
        {
            string option = resolution[i].width + "x" + resolution[i].height;
            dropdown.options.Add(new Dropdown.OptionData(option));
        }
        int presentResoIndex = System.Array.FindIndex(resolution, r => r.width == Screen.width && r.height == Screen.height);
        dropdown.value = presentResoIndex;
    }
    public void ChangeResolution(int index)
    {
        Resolution selectedReso = resolution[index];
        Screen.SetResolution(selectedReso.width, selectedReso.height, Screen.fullScreen);
    }
}