using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOb : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ˮ�������ײ  
        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("��������ˮ������ײ�������彫������");
            Destroy(gameObject); // ���ٻ�����  
        }

       
    }
}
