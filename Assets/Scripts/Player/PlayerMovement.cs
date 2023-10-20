using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float inputMovementH;
    private float inputMovementV;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody> ();
    }
    private void FixedUpdate()
    {
        Vector3 finalMovemnt = new Vector3(inputMovementH, inputMovementV, 0);
        Debug.Log(finalMovemnt);
        rb.AddForce(finalMovemnt * speed, ForceMode.VelocityChange);
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
