using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // 暂停菜单的UI面板
    public static bool GameIsPaused = false;

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

    // 返回主菜单
    public void LoadMainMenu()
    {
        // 恢复时间流动，以免切换到主菜单时时间仍然暂停
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }

    // 退出游戏
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
