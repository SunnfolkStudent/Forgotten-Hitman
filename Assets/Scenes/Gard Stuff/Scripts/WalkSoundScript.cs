using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSoundScript : MonoBehaviour
{

    private AudioSource _AudioSource;
    public AudioClip walkSFX;

    public Input _Input;
    
    
    void Start()
    {
        
    }


    void Update()
    {
        if (_Input.MoveVector > 0f)
        {
            
        }
    }
}
