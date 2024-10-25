using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel3 : MonoBehaviour
{
    public Player player;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level_3_sad");

        }
    }
}
