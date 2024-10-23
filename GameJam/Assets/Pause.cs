using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // ��ͣ�˵���UI���
    public static bool GameIsPaused = false;

    // Update ÿ֡�������
    void Update()
    {
        // ����ESC��ʱ������ͣ
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

    // �ָ���Ϸ
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // ������ͣ�˵�
        Time.timeScale = 1f;          // �ָ���Ϸʱ������
        GameIsPaused = false;         // ������ͣ״̬
    }

    // ��ͣ��Ϸ
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // ��ʾ��ͣ�˵�
        Time.timeScale = 0f;          // ��ͣ��Ϸʱ������
        GameIsPaused = true;          // ������ͣ״̬
    }

    // �������˵�
    public void LoadMainMenu()
    {
        // �ָ�ʱ�������������л������˵�ʱʱ����Ȼ��ͣ
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }

    // �˳���Ϸ
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
