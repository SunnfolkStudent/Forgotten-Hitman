using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float gravity = -13f;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] [Range(0f, 1f)] private float _moveSmoothTime = 0.3f;

    private CharacterController _characterController;
    private Input _input;

    private float _vel;
    private Vector2 _currentDirection = Vector2.zero;
    private Vector2 _currentDirectionVel = Vector2.zero;

    private void Start()
    {
        _input = GetComponent<Input>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateMovementSmooth();
    }
    
    private void UpdateMovementSmooth()
    {
        _currentDirection = Vector2.SmoothDamp(_currentDirection, _input.MoveVector, 
            ref _currentDirectionVel, _moveSmoothTime);

        if (_characterController.isGrounded)
        {
            _vel = -2f;
        }
        else
        {
            _vel += gravity * Time.deltaTime;
        }
        
        _vel = Mathf.Clamp(_vel, -26f, 26f);
 
        //Saves inputted movement & gravity
        var vel = (transform.forward * _currentDirection.y + transform.right * _currentDirection.x) * moveSpeed + Vector3.up * _vel;

        //Moves the character based on vel
        _characterController.Move(vel * Time.deltaTime);
    }
}
