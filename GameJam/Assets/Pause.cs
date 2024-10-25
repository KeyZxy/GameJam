using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // 暂停菜单的UI面板
    public static bool GameIsPaused = false;
    private int currentLevel;  // 当前关卡
    private string currentBackground;  // 当前背景
    // Update 每帧检测输入
    void Update()
    {
        // 按下ESC键时触发暂停
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // 恢复游戏
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // 隐藏暂停菜单
        Time.timeScale = 1f;          // 恢复游戏时间流动
        GameIsPaused = false;         // 更新暂停状态
    }

    // 暂停游戏
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // 显示暂停菜单
        Time.timeScale = 0f;          // 暂停游戏时间流动
        GameIsPaused = true;          // 更新暂停状态
    }

    // 返回主菜单并保存当前进度
    public void SaveAndReturnToMainMenu()
    {
        SaveGameProgress();
        Time.timeScale = 1f;  // 恢复时间
        SceneManager.LoadScene("Start");
    }// 保存当前关卡和背景信息
    void SaveGameProgress()
    {
        // 假设你有一个背景管理器 BackgroundManager 来获取当前背景
        currentLevel = SceneManager.GetActiveScene().buildIndex;  // 获取当前关卡索引
       
        PlayerPrefs.SetInt("SavedLevel", currentLevel);

        PlayerPrefs.Save();  // 保存
    }

    // 退出游戏
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
