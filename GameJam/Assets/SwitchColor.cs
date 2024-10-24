using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColor : MonoBehaviour
{
    public Draw draw;
   // public Player player;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public void Switchred()
    {
        draw.lineColor = Color.red;
        //player.color.Add(Color.red);
        audioSource.PlayOneShot(audioClips[0]);
    }
    public void Switchyellow()
    {
        draw.lineColor = Color.yellow;
        //player.color.Add(Color.yellow);
        audioSource.PlayOneShot(audioClips[1]);
    }
    public void Switchgreen()
    {
        draw.lineColor = Color.green;
        //player.color.Add(Color.green);
        audioSource.PlayOneShot(audioClips[2]);
    }
    public void Switchblue()
    {
        draw.lineColor = Color.blue;
        //player.color.Add(Color.blue);
        audioSource.PlayOneShot(audioClips[3]);
    }
}
