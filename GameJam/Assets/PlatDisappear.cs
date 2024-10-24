using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatDisappear : MonoBehaviour
{
    public float disappearTime = 2f; // ��ʧ��ʱ��
    public float respawnTime = 3f;    // ���³��ֵ�ʱ��

    private TilemapCollider2D tilemapCollider; // Tilemap ����ײ���
    private TilemapRenderer tilemapRenderer;   // Tilemap ����Ⱦ���

    private void Start()
    {
        // ��ȡ�������е� TilemapCollider2D �� TilemapRenderer ���
        tilemapCollider = GetComponent<TilemapCollider2D>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����Ƿ��������ײ
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HandlePlatform());
        }
    }

    private IEnumerator HandlePlatform()
    {
        yield return new WaitForSeconds(disappearTime);

        // ���� Tilemap ����ײ������ҵ���ȥ����ƽ̨��Ȼ�ɼ�
        tilemapCollider.enabled = false;

        // ���� Tilemap ����Ⱦ��ʹƽ̨���Ӿ�����ʧ
        tilemapRenderer.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        // �������� Tilemap ����ײ����Ⱦ��ʹƽ̨���³���
        tilemapCollider.enabled = true;
        tilemapRenderer.enabled = true;
    }
}
