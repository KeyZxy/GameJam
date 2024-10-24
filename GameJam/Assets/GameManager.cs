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
        LoadGameProgress(); // 加载保存的游戏进度
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

    // 加载关卡，使用 Build Index 而不是场景名称
    public void LoadLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        SceneManager.LoadScene(levelIndex); // 使用 Build Index 加载关卡
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            SceneManager.LoadScene(currentLevel); // 继续当前保存的关卡
        }
        else
        {
            LoadLevel(1); // 如果没有保存，加载第一关
        }
    }
}
