using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float angularSpeed;
    [SerializeField] private VirtualJoystick joystick;
    public float speed;
    private Rigidbody rb;

    private float inputMovementH;
    private float inputMovementV;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 finalMovemnt = new Vector3(joystick.Horizontal, joystick.Vertical, 0);

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
