using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start1 : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1_happy");
    }
}
