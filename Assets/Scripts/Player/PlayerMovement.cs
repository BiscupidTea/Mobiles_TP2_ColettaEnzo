using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float angularSpeed;
    [SerializeField] private float speed;
    private Rigidbody rb;
    private float inputMovementH;
    private float inputMovementV;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Vector3 finalMovemnt = new Vector3(inputMovementH, inputMovementV, 0);
        rb.AddForce(finalMovemnt * angularSpeed, ForceMode.VelocityChange);
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }
    public void MovePlayerHorizontal(float input)
    {
        inputMovementH = input;
    }

    public void MovePlayerVertical(float input)
    {
        inputMovementV = input;
    }
}
