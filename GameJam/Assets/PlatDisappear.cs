using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatDisappear : MonoBehaviour
{
    public float disappearTime = 2f; // 消失的时间
    public float respawnTime = 3f;    // 重新出现的时间

    private TilemapCollider2D tilemapCollider; // Tilemap 的碰撞组件
    private TilemapRenderer tilemapRenderer;   // Tilemap 的渲染组件

    private void Start()
    {
        // 获取子物体中的 TilemapCollider2D 和 TilemapRenderer 组件
        tilemapCollider = GetComponent<TilemapCollider2D>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否是玩家碰撞
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HandlePlatform());
        }
    }

    private IEnumerator HandlePlatform()
    {
        yield return new WaitForSeconds(disappearTime);

        // 禁用 Tilemap 的碰撞，让玩家掉下去，但平台仍然可见
        tilemapCollider.enabled = false;

        // 隐藏 Tilemap 的渲染，使平台在视觉上消失
        tilemapRenderer.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        // 重新启用 Tilemap 的碰撞和渲染，使平台重新出现
        tilemapCollider.enabled = true;
        tilemapRenderer.enabled = true;
    }
}
