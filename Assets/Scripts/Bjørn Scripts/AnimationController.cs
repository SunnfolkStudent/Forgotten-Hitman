using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private string[] _animations;

    private void Start() => _animator = GetComponent<Animator>();

    public void Interacting()
    {
        var info = _animator.GetCurrentAnimatorStateInfo(0);
        
        //Prevents changing animation until the current animation has ended
        if (info.normalizedTime < 1) return;
        
        //Plays open animation
        if (info.IsName(_animations[0]) || info.IsName(_animations[2]))
        {
            _animator.Play(_animations[1]);
        }
        //Plays close animation
        else
        {
            _animator.Play((_animations[2]));
        }
    }
}
