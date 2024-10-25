using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void ContinueGame()
    {
        // 从PlayerPrefs加载当前关卡
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            int savedLevel = PlayerPrefs.GetInt("SavedLevel");

            // 加载保存的关卡
            SceneManager.LoadScene(savedLevel);

            // 背景状态会在BackGroundChange脚本的Start方法中自动加载
        }
        else
        {
            // 如果没有保存的进度，加载第一关
            StartNewGame();
        }
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);  // 假设第1关是索引1
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}