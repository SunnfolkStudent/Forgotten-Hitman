using UnityEngine;

public class WalkSoundScript : MonoBehaviour
{

    private AudioSource _AudioSource;
    public AudioClip walkSFX;

    public Input _Input;

    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_Input.MoveVector.magnitude > 0 && _AudioSource.isPlaying == false)
        {
            _AudioSource.pitch = Random.Range(0.8f, 1.1f);
            _AudioSource.PlayOneShot(walkSFX);
        }
    }
    
    
}
