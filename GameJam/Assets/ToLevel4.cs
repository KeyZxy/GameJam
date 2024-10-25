using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ToLevel4 : MonoBehaviour
{
    public Player player;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level_4_fun");

        }
    }
}
