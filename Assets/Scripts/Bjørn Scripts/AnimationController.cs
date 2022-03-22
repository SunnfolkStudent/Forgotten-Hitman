using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private InteractableAudioController _audioController;

    [SerializeField] private string[] _animations;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioController = GetComponent<InteractableAudioController>();
    }

    public void Interacting()
    {
        var info = _animator.GetCurrentAnimatorStateInfo(0);
        
        //Prevents changing animation until the current animation has ended
        if (info.normalizedTime < 1) return;
        
        //Plays open animation
        if (info.IsName(_animations[0]) || info.IsName(_animations[2]))
        {
            _animator.Play(_animations[1]);
            _audioController.SFXOpen();
        }
        //Plays close animation
        else
        {
            _animator.Play((_animations[2]));
            _audioController.SFXClose();
        }
    }
}
