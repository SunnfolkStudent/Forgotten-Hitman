using UnityEngine;

public class Input : MonoBehaviour
{
    private PlayerInputActions _input;
    
    public Vector2 MoveVector { get; private set; }
    public Vector2 LookVector { get; private set; }
    public bool Interact { get; private set; }

    #region InputSetup
    
    private void Awake()
    {
        _input = new PlayerInputActions();
    }
    
    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
    
    #endregion InputSetup
    
    private void Update()
    {
        MoveVector = _input.Player.Move.ReadValue<Vector2>();
        LookVector = _input.Player.Look.ReadValue<Vector2>();
        Interact = _input.Player.Interact.triggered;
        
        if (Interact) print("pressed interact");
    }
}
