using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private bool lockCursor = true;
    [SerializeField] private float mouseSensitivityX = 35f;
    [SerializeField] private float mouseSensitivityY = 25f;

    public bool canLook;

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
        if (!canLook) return;
        
        MouseLookAdvanced();
    }

    private void MouseLookAdvanced()
    {

        _cameraPitch -= _input.LookVector.y * mouseSensitivityY * Time.deltaTime;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90, 90);
        playerCamera.localEulerAngles = Vector3.right * _cameraPitch;
        transform.Rotate(Vector3.up * (_input.LookVector.x * mouseSensitivityX * Time.deltaTime));
    }
}