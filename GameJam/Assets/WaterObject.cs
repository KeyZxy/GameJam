using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : MonoBehaviour
{
    public GameObject[] ladderPrefabs; // ����Ԥ��������  

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
            StartCoroutine(CreateLadders()); // ����Э����������  
            Destroy(gameObject); // ����ˮ����  
        }
    }

    private IEnumerator CreateLadders()
    {
        // ��������Ԥ�������飬����������  
        for (int i = 0; i < ladderPrefabs.Length; i++)
        {
            Instantiate(ladderPrefabs[i], transform.position + new Vector3(0, i * 1.0f, 0), Quaternion.identity); // ����ÿ�����Ӽ��1����λ  
            yield return new WaitForSeconds(0.5f); // �ȴ�0.5��  
        }
    }
}