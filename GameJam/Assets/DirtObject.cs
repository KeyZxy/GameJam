using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtObject : MonoBehaviour
{
    public GameObject ladderPrefab; // ����Ԥ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
      

        // ��������������ײ  
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("ˮ��������������ײ����������");
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
