using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour
{
    // 碰撞进入时调用的方法  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查与水物体的碰撞  
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("火与水碰撞，火物体将被销毁");
            Destroy(gameObject); // 销毁火物体  
        }
    }
}