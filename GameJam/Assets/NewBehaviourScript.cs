using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour
{
    // ��ײ����ʱ���õķ���  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ˮ�������ײ  
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("����ˮ��ײ�������彫������");
            Destroy(gameObject); // ���ٻ�����  
        }
    }
}