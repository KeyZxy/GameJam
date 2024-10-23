using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue : MonoBehaviour
{
    public Text text;
    //public Image image;
    public TextAsset textAsset;
    public int index;
    public float speed;
    bool textfinished;
    bool cancel;
    List<string> list=new List<string>();
    private void Awake()
    {
        GetTextFromfile(textAsset);
    }
    void GetTextFromfile(TextAsset file)
    {
        list.Clear();
        index = 0;
        var Linedata=file.text.Split('\n');
        foreach (var l in Linedata) { 
        list.Add(l);
        }
    }
    private void OnEnable()
    {
        textfinished = true;
        StartCoroutine(SetTextUI());
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) && index == list.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            {
                if (textfinished && !cancel)
                {
                    StartCoroutine(SetTextUI());
                }
                else if (!textfinished && !cancel)
                {
                    cancel = true;
                }

            }
        }
    }
    IEnumerator SetTextUI()
    {
        textfinished=false;
        text.text = "";
        int letter = 0;
        while (!cancel && letter < list[index].Length - 1)
        {
            text.text += list[index][letter];
            letter++;
            yield return new WaitForSeconds(speed);
        }
        text.text = list[index];
        cancel = false;
        textfinished=true;
        index++;
    }
}
