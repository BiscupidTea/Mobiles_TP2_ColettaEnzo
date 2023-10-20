using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    public void OnMoveVertical(InputAction.CallbackContext input)
    {
        float ActualInput = input.ReadValue<float>();
        playerMovement.MovePlayerVertical(ActualInput);
    }

    public void OnMoveHorizontal(InputAction.CallbackContext input)
    {
        float ActualInput = input.ReadValue<float>();
        playerMovement.MovePlayerHorizontal(ActualInput);
    }
}
