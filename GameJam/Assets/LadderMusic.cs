using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMusic : MonoBehaviour
{
    public AudioSource au;
    public AudioClip clip;
    private void Start()
    {
        au.PlayOneShot(clip);
    }
}
