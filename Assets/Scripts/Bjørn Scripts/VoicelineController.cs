using UnityEngine;

public class VoicelineController : MonoBehaviour
{
    [SerializeField] private AudioClip livingRoomVoice;
    [SerializeField] private AudioClip basementVoice;
    [SerializeField] private AudioClip phoneVoice;
    [SerializeField] private AudioClip[] banterVoice;

    [SerializeField] private Collider livingRoomCollider;
    [SerializeField] private Collider bathCollider;
    [SerializeField] private Collider basementCollider;

    private bool playedStart; //Makes sure the start voicelines are only played once
    private bool exitedBath; //Start random banter after player has exited the bathroom
    private bool exitedBasement; //Make sure no voicelines are played after player has exited the basement

    private bool playingAudio; //If set to true, will not start new audio

    private AudioSource _audio;

    private void Start() => _audio = GetComponent<AudioSource>();
    
    private void OnTriggerEnter(Collider other)
    {
        //Prevents any voice lines from playing if player has been in the basement
        if (exitedBasement) return;
        
        print("Entered a trigger");
        
        //Entered Living room
        if (other.CompareTag(livingRoomCollider.tag))
        {
            print("Entered Living Room");
            
            //Plays random banter if you've been in the bathroom
            if (playedStart && exitedBath)
            {
                if (!_audio.isPlaying) PlayVoiceline(banterVoice[Random.Range(0, banterVoice.Length - 1)]);
            }
            //Plays when you first enter the livingroom
            else if (!playedStart)
            {
                PlayVoiceline(livingRoomVoice);
                playedStart = true;
            }
        }
        //Entered Bathroom
        else if (other.CompareTag(bathCollider.tag))
        {
            print("Entered Bathroom");

            exitedBath = true;
        }
        //Entered Basement
        else if (other.CompareTag(basementCollider.tag))
        {
            print("Entered Basement");
            
            PlayVoiceline();
            exitedBasement = true;
        }
    }

    private void PlayVoiceline(AudioClip voiceline)
    {
        _audio.clip = voiceline;
        _audio.Play();
    }
}