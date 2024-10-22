using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMove : MonoBehaviour
{
    
    public Transform[] patrolPoints; // 巡逻路径上的各个路点  
    public float speed = 2f;
    private int currentPatrolIndex = 0; // 当前巡逻点的索引  

    void Update() // 使用 Update 方法来持续调用巡逻  
    {
        Patrol(); // 每帧调用 Patrol 方法  
    }

    void Patrol() // 敌人在路点之间巡逻  
    {
        if (patrolPoints.Length == 0) return; // 没有设置路点时，直接返回  

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // 将新位置赋值给平台的 transform.position  
        transform.position = newPosition;

        // 当敌人到达当前路点时，转向下一个路点  
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // 依次循环路点  
        }
    }
    
}