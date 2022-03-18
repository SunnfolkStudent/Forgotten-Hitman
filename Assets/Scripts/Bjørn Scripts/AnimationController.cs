using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private string[] _animations;

    private void Start() => _animator = GetComponent<Animator>();

    private void PlayAnimation(int animationNum)
    {
        _animator.Play(_animations[animationNum]);
    }

    public void Interacting()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_animations[0]) || _animator.GetCurrentAnimatorStateInfo(0).IsName(_animations[2]))
        {
            _animator.Play(_animations[1]);
        }
        else
        {
            _animator.Play((_animations[2]));
        }
    }
}
