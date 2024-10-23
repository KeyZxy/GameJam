using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtObject : MonoBehaviour
{
    public GameObject ladderPrefab; // ����Ԥ����  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ˮ�������ײ  
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("ˮ��������������ײ����������");
            StartCoroutine(CreateLadder()); // ����Э����������  
            Destroy(collision.gameObject); // ����ˮ����  
        }
    }

    private IEnumerator CreateLadder()
    {
        // ��������Ԥ����  
        GameObject ladder = Instantiate(ladderPrefab, transform.position, Quaternion.identity);

        // ��ȡ����Ԥ�����������  
        Transform[] ladderParts = ladder.GetComponentsInChildren<Transform>(true);

        // ȷ��ֻ��������������������Ը����屾��  
        foreach (Transform part in ladderParts)
        {
            if (part != ladder.transform)
            {
                part.gameObject.SetActive(false); // ��ʼ��������������  
            }
        }

        // ������ʾ���ӵ�������  
        foreach (Transform part in ladderParts)
        {
            if (part != ladder.transform)
            {
                part.gameObject.SetActive(true); // ������ʾÿ��������  
                yield return new WaitForSeconds(0.5f); // ÿ�����0.5����ʾ  
            }
        }

        Destroy(gameObject);
    }
}