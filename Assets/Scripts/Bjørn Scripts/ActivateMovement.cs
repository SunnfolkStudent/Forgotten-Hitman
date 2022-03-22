using UnityEngine;

public class ActivateMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement _Movement;
    [SerializeField] private PlayerLook _Look;
    
    public void UnlockMovement()
    {
        _Movement.canMove = true;
    }
    
    public void LockMovement()
    {
        _Movement.canMove = false;
    }
    
    private void UnlockSight()
    {
        _Look.canLook = true;
    }
    
    private void LockSight()
    {
        _Look.canLook = false;
    }
}
