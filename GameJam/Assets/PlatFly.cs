using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatFly : MonoBehaviour
{
    public Transform targetPosition;  // Ѳ��·���ϵĸ���·��
    public float moveSpeed = 5f;      // �ƶ��ٶ�
    private bool shouldMove = false;  // ��־�Ƿ�Ӧ���ƶ�

    private Rigidbody2D rb;

    void Start()
    {
        // ��ȡRigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // ��� targetPosition Ϊ�գ���������
        if (targetPosition == null)
        {
            Debug.LogWarning("Target position is not assigned!");
        }
    }

    void Update()
    {
        // ��� shouldMove �Ƿ�Ϊ true ���� targetPosition �Ƿ�������
        if (shouldMove && targetPosition != null)
        {
            // ƽ���ƶ���Ŀ��λ��
            transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

            // ����Ƿ�ӽ�Ŀ��λ��
            if (Vector2.Distance(transform.position, targetPosition.position) < 0.01f)
            {
                shouldMove = false;  // ֹͣ�ƶ�
                //Debug.Log("Reached target position");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ����Ƿ������ "Air" ��ǩ�����巢����ײ
        if (collision.gameObject.CompareTag("Air"))
        {
            
            shouldMove = true;  // ��ʼ�ƶ�
        }
    }
}
