using UnityEngine;

public class ActivateMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement _Movement;

    public void UnlockMovement()
    {
        _Movement.canMove = true;
        print("Movement is unlocked");
    }
    
    public void LockMovement()
    {
        _Movement.canMove = false;
    }
}
