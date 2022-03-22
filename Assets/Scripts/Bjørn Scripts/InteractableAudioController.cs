using UnityEngine;

public class InteractableAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _sfxOpen;
    [SerializeField] private AudioClip _sfxClose;

    private AudioSource _Source;

    private void Start() => _Source = GetComponent<AudioSource>();
    

    public void SFXOpen()
    {
        _Source.PlayOneShot(_sfxOpen);
    }
    
    public void SFXClose()
    {
        _Source.PlayOneShot(_sfxClose);
    }
}
