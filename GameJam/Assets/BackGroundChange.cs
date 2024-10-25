using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundChange : MonoBehaviour
{
    public GameObject player;
    public Player playercolor; 
    public SpriteRenderer[] bg; 
    public CameraLookat cameraLookat;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playercolor = player.GetComponent<Player>();

    }


    void Update()
    {

        if (playercolor.color != null && playercolor.color.Count > 0)
        {

            bg[1].enabled=true;
            cameraLookat.ChangeBackground(bg[1]);
            bg[0].enabled=false;
           
        }
        else
        {

            bg[0].enabled = true;
            cameraLookat.ChangeBackground(bg[0]);
            bg[1].enabled = false;

        }
    }
   
}
