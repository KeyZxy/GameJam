using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer background;
    public SpriteRenderer character;

    public Sprite[] backgroundSprites;
    public Sprite[] characterSprites;

    private int currentLevel = 1;

    private void Start()
    {
        LoadGameProgress(); // ���ر������Ϸ����
    }

    public void SaveGameProgress()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("CurrentBackground", background.sprite.GetInstanceID());
        PlayerPrefs.SetInt("CurrentCharacter", character.sprite.GetInstanceID());
        PlayerPrefs.Save();
    }

    public void LoadGameProgress()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            int bgID = PlayerPrefs.GetInt("CurrentBackground");
            int charID = PlayerPrefs.GetInt("CurrentCharacter");

            background.sprite = FindSpriteByID(bgID, backgroundSprites);
            character.sprite = FindSpriteByID(charID, characterSprites);
        }
    }

    private Sprite FindSpriteByID(int id, Sprite[] spriteArray)
    {
        foreach (var sprite in spriteArray)
        {
            if (sprite.GetInstanceID() == id)
                return sprite;
        }
        return null;
    }

    // ���عؿ���ʹ�� Build Index �����ǳ�������
    public void LoadLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        SceneManager.LoadScene(levelIndex); // ʹ�� Build Index ���عؿ�
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            SceneManager.LoadScene(currentLevel); // ������ǰ����Ĺؿ�
        }
        else
        {
            LoadLevel(1); // ���û�б��棬���ص�һ��
        }
    }
}
