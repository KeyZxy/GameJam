using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer background;
    public SpriteRenderer character; //��ɫ�� SpriteRenderer
     public SpriteRenderer[] backgroundSprites; // ���� Sprite ����
     public SpriteRenderer[] characterSprites; //��ɫ Sprite ����
     private int currentLevel =1;  

    private void Start()
    {
        LoadGameProgress(); // ���ر������Ϸ����
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
                    return i; // ��������
                              }  
                return -1; // ���û���ҵ������� -1
                           }  

        private Sprite FindSpriteByID(int id, SpriteRenderer[] spriteArray)
                {
                    foreach (var sprite in spriteArray)
                    {
                        if (sprite.GetInstanceID() == id)
                            return sprite.sprite; // ���ض�Ӧ�� sprite }
                                                  }
                        return null;
                    }

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
                                SceneManager.LoadScene(currentLevel); //������ǰ����Ĺؿ�
                                                                     }  
 else
                                {
                                    LoadLevel(1); // ���û�б��棬���ص�һ��
                                                  }  
                                }

                                private void OnApplicationQuit()
                                {
                                    SaveGameProgress(); // ��Ӧ���˳�ʱ������Ϸ����
                                                        }  
                                }