using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    // ��ײ����ʱ���õķ���  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb =collision.gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
