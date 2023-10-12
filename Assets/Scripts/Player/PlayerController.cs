using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    public void OnMove(InputAction.CallbackContext input)
    {
        Vector2 ActualInput = input.ReadValue<Vector2>();
        playerMovement.MovePlayer(ActualInput);
    }
}
