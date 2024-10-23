using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;

public class NOBoaderWindow : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string strClassname, int nptWindowname);
    [DllImport("user32.dll")]
    static extern IntPtr GetForgroundWindow();
    [DllImport("user32.dll")] 
    static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint uflags);
    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int newLong);

    const uint ShowWindow = 0x0040;
    const int GWLstyle = -16;
    const int WSborder = 1;
    Resolution[] reso;  //分辨率
    private Rect screenPos; //最终屏幕的位置和长宽

    private void Awake()
    {
        reso = Screen.resolutions; 
    }
    public void OnClick()
    {
        witnOutBorder();
        Setpos();
    }
    public Rect GetWindowInfo()     //获取当前窗口尺寸
    {
        RECT rect = new RECT();
        Rect targetRect = new Rect();
        GetWindowRect(GetForgroundWindow(), ref rect);
        targetRect.width = Mathf.Abs(rect.right - rect.left);
        targetRect.height = Math.Abs(rect.top - rect.bottom);
        targetRect.x = rect.left;
        targetRect.y = rect.top;
        return targetRect;
    }
    private int GetTaskBarHeight()   //获取任务栏高度
    {
        int taskbarHeight = 10;
        IntPtr hWnd = FindWindow("Shell_TrayWnd", 0);   //找到任务栏窗口
        RECT rect = new RECT();
        GetWindowRect(hWnd, ref rect);
        taskbarHeight = (int)(rect.bottom - rect.top);
        return taskbarHeight;
    }
    private void witnOutBorder()    //无边框
    {
        screenPos.width = reso[reso.Length - 1].width;
        //新的屏幕高度
        int preMaxScreenHeight = Screen.currentResolution.height - GetTaskBarHeight();
        screenPos.height = preMaxScreenHeight;
        //新的分辨率
        Screen.SetResolution((int)screenPos.width, (int)screenPos.height, false);
        screenPos.x = (int)((Screen.currentResolution.width - screenPos.width) / 2);    //宽度居中
        screenPos.y = (int)((Screen.currentResolution.height - screenPos.height) / 2);  //高度居中
        //设置无框
        SetWindowLong(GetForgroundWindow(), GWLstyle, WSborder);
        //居中显示
        bool result = SetWindowPos(GetForgroundWindow(), 0, (int)screenPos.x, (int)screenPos.y, (int)screenPos.width, (int)screenPos.height, ShowWindow);
    }
    private void Setpos()
    {
        SetWindowLong(GetForgroundWindow(), GWLstyle, WSborder);
        bool result = SetWindowPos(GetForgroundWindow(), 0, 0, 0, reso[reso.Length - 1].width, reso[reso.Length - 1].height, ShowWindow);
    }
}