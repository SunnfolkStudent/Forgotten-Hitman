using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class VoicelineController : MonoBehaviour
{
    [SerializeField] private GameObject character3;
    [SerializeField] private AudioSource bathSource;
    [SerializeField] private AudioClip openBathAudio;
    [SerializeField] private GameObject bathDoor;
    
    [SerializeField] private AudioClip livingRoomVoice;
    [SerializeField] private AudioClip basementVoice;
    [SerializeField] private AudioClip phoneVoice;
    [SerializeField] private AudioClip[] banterVoice;

    [SerializeField] private Collider livingRoomCollider;
    [SerializeField] private Collider basementCollider;
    [SerializeField] private Collider exitBasementCollider;
    
    [SerializeField] private AudioSource livingRoomSource;

    [SerializeField] private InteractScript _interact;
    [SerializeField] private FadeScreenScript _fade;
    [SerializeField] private PlayerMovement _move;
    [SerializeField] private PlayerLook _look;

    private bool playedStart; //Makes sure the start voicelines are only played once
    private bool playedBasement; //Makes sure the start voicelines are only played once
    private bool playedEnd; //Makes sure the start voicelines are only played once
    private bool bathOpened;
    private bool exitedBasement;

    private bool enteredBasement; //Make sure no voicelines are played after player has exited the basement

    private bool playingAudio; //If set to true, will not start new audio

    private float endSceneTimer = 15f;
    private float backupTimer = 15f;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        character3.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Prevents any voice lines from playing if player has been in the basement
        if (enteredBasement)
        {
            if (other.CompareTag(exitBasementCollider.tag)) exitedBasement = true;
            return;
        }
        
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
                if (exitedBasement) endSceneTimer = 0;
            }
            //Plays end scene
            else if (!playedEnd)
            {
                PlayVoiceline(phoneVoice, _audio);
                _move.canMove = false;
                _look.canLook = false;
                playedEnd = true;
            }

            if (playedEnd && backupTimer > 0) backupTimer -= Time.deltaTime;
            else if (playedEnd) SceneManager.LoadScene("The End Scene");
        }

        if (playedEnd && !_audio.isPlaying)
        {
            _fade.FadeOutLoadEnd();
        }

        if (playedStart && !bathOpened)
        {
            print("start, but no bath");
            if (!livingRoomSource.isPlaying)
            {
                OpenBath();
            }
        }
        
        if (_interact.hasInteractedWithShower) character3.SetActive(true);
    }

    private void PlayVoiceline(AudioClip voiceline, AudioSource source)
    {
        source.clip = voiceline;
        source.Play();
    }

    private void OpenBath()
    {
        bathSource.loop = false;
        PlayVoiceline(openBathAudio, bathSource);
        bathOpened = true;
        bathDoor.GetComponent<AnimationController>().Interacting();
    }
}