using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public AudioSource au;
    public AudioClip ex;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dirt"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            au.PlayOneShot(ex);
        }
    }
}
