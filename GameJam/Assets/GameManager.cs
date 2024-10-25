using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
 

    void Start()
    {
        
    }
    public void ContinueGame()
    {
        FindObjectOfType<GameManager>().LoadSavedGame();

    }
    
    public void LoadSavedGame()
    {
        // 加载保存的关卡和背景
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            int savedLevel = PlayerPrefs.GetInt("SavedLevel");
            SceneManager.LoadScene(savedLevel);
        }
    }
}