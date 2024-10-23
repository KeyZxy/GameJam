using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : MonoBehaviour
{
    public GameObject ladderPrefab; // 梯子预制体

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查与火物体的碰撞  
        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("火粒子与水物体碰撞，水物体将被销毁");
            Destroy(gameObject); // 销毁水物体  
        }

        // 检查与土物体的碰撞  
        if (collision.gameObject.CompareTag("Dirt"))
        {
            Debug.Log("土粒子与水物体碰撞，生成梯子");
            CreateLadder(); // 生成梯子
            Destroy(gameObject); // 销毁水物体  
        }
    }

    void CreateLadder()
    {
        // 在水物体的位置生成梯子
        Instantiate(ladderPrefab, transform.position, Quaternion.identity);
    }
}
