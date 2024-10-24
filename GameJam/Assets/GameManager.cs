using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer background;
    public SpriteRenderer character; //角色的 SpriteRenderer
     public SpriteRenderer[] backgroundSprites; // 背景 Sprite 数组
     public SpriteRenderer[] characterSprites; //角色 Sprite 数组
     private int currentLevel =1;  

    private void Start()
    {
        LoadGameProgress(); // 加载保存的游戏进度
     }  

        public void SaveGameProgress()
        {
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            PlayerPrefs.SetInt("CurrentBackground", FindSpriteIndex(background, backgroundSprites));
            PlayerPrefs.SetInt("CurrentCharacter", FindSpriteIndex(character, characterSprites));
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

        private int FindSpriteIndex(SpriteRenderer spriteRenderer, SpriteRenderer[] spriteArray)
        {
            for (int i = 0; i < spriteArray.Length; i++)
            {
                if (spriteArray[i] == spriteRenderer)
                    return i; // 返回索引
                              }  
                return -1; // 如果没有找到，返回 -1
                           }  

        private Sprite FindSpriteByID(int id, SpriteRenderer[] spriteArray)
                {
                    foreach (var sprite in spriteArray)
                    {
                        if (sprite.GetInstanceID() == id)
                            return sprite.sprite; // 返回对应的 sprite }
                                                  }
                        return null;
                    }

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
                                SceneManager.LoadScene(currentLevel); //继续当前保存的关卡
                                                                     }  
 else
                                {
                                    LoadLevel(1); // 如果没有保存，加载第一关
                                                  }  
                                }

                                private void OnApplicationQuit()
                                {
                                    SaveGameProgress(); // 在应用退出时保存游戏进度
                                                        }  
                                }