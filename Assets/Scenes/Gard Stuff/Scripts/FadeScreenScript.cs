using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreenScript : MonoBehaviour
{
    public GameObject FadeScreen;
    public AudioClip wakeupSFX;
    private AudioSource _AudioSource;

    public GameObject FadeOutScreen;
    public AudioClip showerSFX;

    public GameObject BlackScreen;
    
    public InteractScript _InteractScript;

    private bool showerloop;

    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        _AudioSource.PlayOneShot(wakeupSFX);
        Invoke("FadeIn", wakeupSFX.length);
        showerloop = false;
        BlackScreen.SetActive(false);
    }

    private void Update()
    {
        Showerfade();
    }

    private void Showerfade()
    {
        if (_InteractScript.hasInteractedWithShower && !showerloop)
        {
            _AudioSource.PlayOneShot(showerSFX);
            Invoke("FadeOut", 0.1f);
            Invoke("ShowerFadeFix", 1.5f);
            BlackScreen.SetActive(false);
            Invoke("FadeIn", showerSFX.length + 1f);
            showerloop = true;
        }
        
    }
    
    private void FadeIn()
    {
        FadeScreen.GetComponent<Animation>().Play("Fade Animation");
    }

    private void FadeOut()
    {
        FadeOutScreen.GetComponent<Animation>().Play("FadeOutAnimation");
    }

    private void ShowerFadeFix()
    {
        FadeOutScreen.SetActive(false);
        BlackScreen.SetActive(true);
        Invoke("ShowerBlackfix", showerSFX.length);
    }

    private void ShowerBlackfix()
    {
        BlackScreen.SetActive(false);
    }
}
