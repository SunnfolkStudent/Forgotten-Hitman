using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreenScript : MonoBehaviour
{
    public GameObject FadeScreen;
    public AudioClip wakeupSFX;
    private AudioSource _AudioSource;

    


    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        _AudioSource.PlayOneShot(wakeupSFX);
        Invoke("FadeIn", wakeupSFX.length + 0.1f);



    }

    public void FadeIn()
    {
        FadeScreen.GetComponent<Animation>().Play("Fade Animation");
    }
}
