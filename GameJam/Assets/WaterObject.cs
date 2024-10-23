using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : MonoBehaviour
{
    public GameObject[] ladderPrefabs; // 梯子预制体数组  

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
            StartCoroutine(CreateLadders()); // 启动协程生成梯子  
            Destroy(gameObject); // 销毁水物体  
        }
    }

    private IEnumerator CreateLadders()
    {
        // 遍历梯子预制体数组，逐渐生成梯子  
        for (int i = 0; i < ladderPrefabs.Length; i++)
        {
            Instantiate(ladderPrefabs[i], transform.position + new Vector3(0, i * 1.0f, 0), Quaternion.identity); // 假设每个梯子间隔1个单位  
            yield return new WaitForSeconds(0.5f); // 等待0.5秒  
        }
    }
}