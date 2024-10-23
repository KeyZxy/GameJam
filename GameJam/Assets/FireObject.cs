using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour
{
    public ParticleSystem bomb; // 爆炸粒子系统
    public float explosionRadius = 2f; // 爆炸影响的半径

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查与水物体的碰撞  
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("火物体与水粒子碰撞，火物体将被销毁");
            Destroy(gameObject); // 销毁火物体  
        }

        // 检查与气体物体的碰撞  
        if (collision.gameObject.CompareTag("Air"))
        {
            Debug.Log("火物体与气体粒子碰撞，触发爆炸粒子特效并销毁火物体");
            CreateBomb(); // 触发爆炸粒子特效
            Destroy(gameObject); // 销毁火物体  
        }
    }

    void CreateBomb()
    {
        // 在当前物体的位置生成粒子特效  
        ParticleSystem instantiatedBomb = Instantiate(bomb, transform.position, Quaternion.identity);
        //instantiatedBomb.Play(); // 播放粒子系统 

        // 查找爆炸范围内的所有物体
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D obj in objectsInRange)
        {
            if (obj.CompareTag("Dirt"))
            {
                Debug.Log("爆炸范围内的土物体被销毁：" + obj.name);
                Destroy(obj.gameObject); // 销毁土物体  
            }
        }

        // 销毁粒子系统对象，在粒子播放结束后自动清除
        Destroy(instantiatedBomb.gameObject, instantiatedBomb.main.duration + instantiatedBomb.main.startLifetime.constantMax);
    }

    // 可视化爆炸范围，方便调试
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
