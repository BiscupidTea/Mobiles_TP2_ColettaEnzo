using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 inputMovement;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody> ();
    }
    private void FixedUpdate()
    {
        Vector3 finalMovemnt = new Vector3(inputMovement.x, inputMovement.y, 0);
        rb.AddForce(finalMovemnt * speed, ForceMode.VelocityChange);
    }
    public void MovePlayer(Vector2 input)
    {
        inputMovement = input;
    }
}
