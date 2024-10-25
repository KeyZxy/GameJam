using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // ��ͣ�˵���UI���
    public static bool GameIsPaused = false;
    private int currentLevel;  // ��ǰ�ؿ�
    private string currentBackground;  // ��ǰ����
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

    // �������˵������浱ǰ����
    public void SaveAndReturnToMainMenu()
    {
        SaveGameProgress();
        Time.timeScale = 1f;  // �ָ�ʱ��
        SceneManager.LoadScene("Start");
    }// ���浱ǰ�ؿ��ͱ�����Ϣ
    void SaveGameProgress()
    {
        // ��������һ������������ BackgroundManager ����ȡ��ǰ����
        currentLevel = SceneManager.GetActiveScene().buildIndex;  // ��ȡ��ǰ�ؿ�����
       
        PlayerPrefs.SetInt("SavedLevel", currentLevel);

        PlayerPrefs.Save();  // ����
    }

    // �˳���Ϸ
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
