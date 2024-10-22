using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMove : MonoBehaviour
{
    
    public Transform[] patrolPoints; // Ѳ��·���ϵĸ���·��  
    public float speed = 2f;
    private int currentPatrolIndex = 0; // ��ǰѲ�ߵ������  

    void Update() // ʹ�� Update ��������������Ѳ��  
    {
        Patrol(); // ÿ֡���� Patrol ����  
    }

    void Patrol() // ������·��֮��Ѳ��  
    {
        if (patrolPoints.Length == 0) return; // û������·��ʱ��ֱ�ӷ���  

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // ����λ�ø�ֵ��ƽ̨�� transform.position  
        transform.position = newPosition;

        // �����˵��ﵱǰ·��ʱ��ת����һ��·��  
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // ����ѭ��·��  
        }
    }
    
}