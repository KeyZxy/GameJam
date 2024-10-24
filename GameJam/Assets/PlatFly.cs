using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatFly : MonoBehaviour
{
    public Transform targetPosition;  // 巡逻路径上的各个路点
    public float moveSpeed = 5f;      // 移动速度
    private bool shouldMove = false;  // 标志是否应该移动

    private Rigidbody2D rb;

    void Start()
    {
        // 获取Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // 如果 targetPosition 为空，发出警告
        if (targetPosition == null)
        {
            Debug.LogWarning("Target position is not assigned!");
        }
    }

    void Update()
    {
        // 检查 shouldMove 是否为 true 并且 targetPosition 是否已设置
        if (shouldMove && targetPosition != null)
        {
            // 平滑移动到目标位置
            transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

            // 检查是否接近目标位置
            if (Vector2.Distance(transform.position, targetPosition.position) < 0.01f)
            {
                shouldMove = false;  // 停止移动
                //Debug.Log("Reached target position");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否与带有 "Air" 标签的物体发生碰撞
        if (collision.gameObject.CompareTag("Air"))
        {
            
            shouldMove = true;  // 开始移动
        }
    }
}
