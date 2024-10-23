using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtObject : MonoBehaviour
{
    public GameObject ladderPrefab; // 梯子预制体
    private void OnCollisionEnter2D(Collision2D collision)
    {
      

        // 检查与土物体的碰撞  
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("水粒子与土物体碰撞，生成梯子");
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
