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
        else
        {
            // 如果没有保存的进度，加载第一关
            StartNewGame();
        }
    } public void StartNewGame()
        {
            SceneManager.LoadScene(1);  // 假设第1关是索引1
        }
    }
