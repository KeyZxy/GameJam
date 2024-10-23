using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : MonoBehaviour
{
    public GameObject ladderPrefab; // ����Ԥ����

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �������������ײ  
        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("��������ˮ������ײ��ˮ���彫������");
            Destroy(gameObject); // ����ˮ����  
        }

        // ��������������ײ  
        if (collision.gameObject.CompareTag("Dirt"))
        {
            Debug.Log("��������ˮ������ײ����������");
            CreateLadder(); // ��������
            Destroy(gameObject); // ����ˮ����  
        }
    }

    void CreateLadder()
    {
        // ��ˮ�����λ����������
        Instantiate(ladderPrefab, transform.position, Quaternion.identity);
    }
}
