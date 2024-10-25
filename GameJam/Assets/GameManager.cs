using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void ContinueGame()
    {
        // ��PlayerPrefs���ص�ǰ�ؿ�
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            int savedLevel = PlayerPrefs.GetInt("SavedLevel");

            // ���ر���Ĺؿ�
            SceneManager.LoadScene(savedLevel);

            // ����״̬����BackGroundChange�ű���Start�������Զ�����
        }
        else
        {
            // ���û�б���Ľ��ȣ����ص�һ��
            StartNewGame();
        }
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);  // �����1��������1
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}