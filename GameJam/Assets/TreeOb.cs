using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOb : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查与水物体的碰撞  
        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("火物体与水粒子碰撞，火物体将被销毁");
            Destroy(gameObject); // 销毁火物体  
        }

       
    }
}
