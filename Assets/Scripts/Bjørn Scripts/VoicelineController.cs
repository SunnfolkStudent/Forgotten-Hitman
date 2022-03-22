using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoicelineController : MonoBehaviour
{
    [SerializeField] private AudioClip livingRoomVoice;
    [SerializeField] private AudioClip basementVoice;
    [SerializeField] private AudioClip phoneVoice;
    [SerializeField] private AudioClip[] banterVoice;

    [SerializeField] private Collider livingRoomCollider;
    [SerializeField] private Collider basementCollider;
    
    [SerializeField] private AudioSource livingRoomSource;

    [SerializeField] private InteractScript _interact;
    [SerializeField] private FadeScreenScript _fade;

    private bool playedStart; //Makes sure the start voicelines are only played once
    private bool playedBasement; //Makes sure the start voicelines are only played once
    private bool playedEnd; //Makes sure the start voicelines are only played once
    
    private bool enteredBasement; //Make sure no voicelines are played after player has exited the basement

    private bool playingAudio; //If set to true, will not start new audio

    private float endSceneTimer = 3f;

    private AudioSource _audio;

    private void Start() => _audio = GetComponent<AudioSource>();
    
    private void OnTriggerEnter(Collider other)
    {
        //Prevents any voice lines from playing if player has been in the basement
        if (enteredBasement) return;
        
        print("Entered a trigger");
        
        //Entered Living room
        if (other.CompareTag(livingRoomCollider.tag))
        {
            print("Entered Living Room");
            
            //Plays if entering living room for a second/third/more times
            if (playedStart && _interact.hasInteractedWithShower)
            {
                //Plays key hinting
                if (!playedBasement)
                {
                    PlayVoiceline(basementVoice, livingRoomSource);
                    playedBasement = true;
                }
                //Plays random banter
                else if (!_audio.isPlaying) PlayVoiceline(banterVoice[Random.Range(0, banterVoice.Length - 1)], livingRoomSource);
            }
            //Plays when you first enter the livingroom
            else if (!playedStart)
            {
                PlayVoiceline(livingRoomVoice, livingRoomSource);
                playedStart = true;
            }
        }
        //Entered Basement
        else if (other.CompareTag(basementCollider.tag))
        {
            print("Entered Basement");
            
            enteredBasement = true;
        }
    }

    private void Update()
    {
        if (enteredBasement)
        {
            if (endSceneTimer > 0)
            {
                endSceneTimer -= Time.deltaTime;
            }
            else if (!playedEnd)
            {
                PlayVoiceline(phoneVoice, _audio);
                playedEnd = true;
            }
        }

        if (playedEnd && !_audio.isPlaying)
        {
            _fade.FadeIn();
        }
    }

    private void PlayVoiceline(AudioClip voiceline, AudioSource source)
    {
        source.clip = voiceline;
        source.Play();
    }
}