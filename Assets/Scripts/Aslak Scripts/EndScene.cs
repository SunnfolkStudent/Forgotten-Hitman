using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public Animator TheEnd;
    private void Awake()
    {
        TheEnd.Play("TheEndFadeIn");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SindreTestScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
