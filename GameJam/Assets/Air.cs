using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    public AudioSource au;
    public AudioClip ex;
    // 碰撞进入时调用的方法  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            au.PlayOneShot(ex);
        }
    }
}
