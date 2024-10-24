using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirObject : MonoBehaviour
{
    public AudioSource au;
    public AudioClip ex;
    public ParticleSystem bomb; // ��ը����ϵͳ
    public float explosionRadius = 2f; // ��ըӰ��İ뾶
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����������������ײ  
        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("������������������ײ��������ը������Ч��������������");
            CreateBomb(); // ������ը������Ч
            au.PlayOneShot(ex);
            Destroy(gameObject); // ������������  
        }
    }
    void CreateBomb()
    {
        // �ڵ�ǰ�����λ������������Ч  
        ParticleSystem instantiatedBomb = Instantiate(bomb, transform.position, Quaternion.identity);
        //instantiatedBomb.Play(); // ��������ϵͳ 

        // ���ұ�ը��Χ�ڵ���������
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D obj in objectsInRange)
        {
            if (obj.CompareTag("Dirt"))
            {
                Debug.Log("��ը��Χ�ڵ������屻���٣�" + obj.name);
                Destroy(obj.gameObject); // ����������  
            }
        }

        // ��������ϵͳ���������Ӳ��Ž������Զ����
        Destroy(instantiatedBomb.gameObject, instantiatedBomb.main.duration + instantiatedBomb.main.startLifetime.constantMax);
    }

    // ���ӻ���ը��Χ���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
