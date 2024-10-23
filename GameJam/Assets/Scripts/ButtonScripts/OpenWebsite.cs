using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class OpenWebsite : MonoBehaviour
{
    public static Process process;
    public Button btn;
    [Header("µÚÈý·½ÍøÕ¾ÍøÖ·")]public string web;
    private void Start()
    {
        btn.onClick.AddListener(OpenURL);
    }
    public void OpenURL()
    {
        Application.OpenURL(web);
    }
}