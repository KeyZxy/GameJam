using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour
{
    public ParticleSystem bomb; // ��ը����ϵͳ
    public float explosionRadius = 2f; // ��ըӰ��İ뾶

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ˮ�������ײ  
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("��������ˮ������ײ�������彫������");
            Destroy(gameObject); // ���ٻ�����  
        }

        // ����������������ײ  
        if (collision.gameObject.CompareTag("Air"))
        {
            Debug.Log("������������������ײ��������ը������Ч�����ٻ�����");
            CreateBomb(); // ������ը������Ч
            Destroy(gameObject); // ���ٻ�����  
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
