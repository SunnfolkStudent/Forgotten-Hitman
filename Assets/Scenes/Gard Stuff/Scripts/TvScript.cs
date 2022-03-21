using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvScript : MonoBehaviour
{
    public InteractScript _InteractScript;
    public GameObject NewsImage;
    public AudioClip NewsSpeech;
    private AudioSource _AudioSource;
    private bool TVInteractFix;


    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        NewsImage.SetActive(false);
        TVInteractFix = false;
    }

    private void Update()
    {
        if (_InteractScript.hasInteractedWithTV && !TVInteractFix)
        {
            NewsImage.SetActive(true);
            _AudioSource.PlayOneShot(NewsSpeech);
            TVInteractFix = true;
        }
        
    }
}
