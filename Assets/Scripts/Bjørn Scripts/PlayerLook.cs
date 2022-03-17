using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private bool lockCursor = true;
    [SerializeField] private float mouseSensitivityX = 35f;
    [SerializeField] private float mouseSensitivityY = 25f;
    [SerializeField] [Range(0f, 0.5f)] private float mouseSmoothTime = 0.03f;

    private Vector2 _currentMouseDelta = Vector2.zero;
    private Vector2 _currentMouseVelocity = Vector2.zero;

    private float _cameraPitch;
    private Input _input;

    private void Start()
    {
        _input = GetComponent<Input>();
        if (lockCursor) Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MouseLookAdvanced();
    }

    private void MouseLookAdvanced()
    {
        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, _input.LookVector,
            ref _currentMouseVelocity, mouseSmoothTime);

        _cameraPitch -= _currentMouseDelta.y * mouseSensitivityY * Time.deltaTime;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90, 90);
        playerCamera.localEulerAngles = Vector3.right * _cameraPitch;
        transform.Rotate(Vector3.up * _currentMouseDelta.x * mouseSensitivityX * Time.deltaTime);
    }
}
