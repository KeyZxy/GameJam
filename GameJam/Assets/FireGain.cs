using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGain : MonoBehaviour
{
    public Player player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
         
        if (collision.gameObject.CompareTag("Player"))
        {
            player.AddColor(Color.red);
            Destroy(gameObject);
        }
    }
}
