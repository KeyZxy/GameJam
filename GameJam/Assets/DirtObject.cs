using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtObject : MonoBehaviour
{
    public GameObject ladderPrefab; // 梯子预制体  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查与水物体的碰撞  
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("水粒子与土物体碰撞，生成梯子");
            StartCoroutine(CreateLadder()); // 启动协程生成梯子  
            Destroy(collision.gameObject); // 销毁水物体  
        }
    }

    private IEnumerator CreateLadder()
    {
        // 生成梯子预制体  
        GameObject ladder = Instantiate(ladderPrefab, transform.position, Quaternion.identity);

        // 获取梯子预制体的子物体  
        Transform[] ladderParts = ladder.GetComponentsInChildren<Transform>(true);

        // 确保只控制子物体的显隐，忽略父物体本身  
        foreach (Transform part in ladderParts)
        {
            if (part != ladder.transform)
            {
                part.gameObject.SetActive(false); // 初始隐藏所有子物体  
            }
        }

        // 依次显示梯子的子物体  
        foreach (Transform part in ladderParts)
        {
            if (part != ladder.transform)
            {
                part.gameObject.SetActive(true); // 依次显示每个子物体  
                yield return new WaitForSeconds(0.5f); // 每个间隔0.5秒显示  
            }
        }

        Destroy(gameObject);
    }
}