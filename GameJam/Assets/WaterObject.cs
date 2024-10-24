using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : MonoBehaviour
{
    public GameObject ladderPrefab; // ����Ԥ��������  
    public AudioSource au;
    public AudioClip ex;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �������������ײ  
        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("��������ˮ������ײ��ˮ���彫������");
            au.PlayOneShot(ex);
            Destroy(gameObject); // ����ˮ����  
        }

        // ��������������ײ  
        if (collision.gameObject.CompareTag("Dirt"))
        {
            Debug.Log("��������ˮ������ײ����������");
            
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