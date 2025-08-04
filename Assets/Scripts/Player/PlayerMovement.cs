using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private float moveSpeed = 5f;

    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(movement.x, movement.y, 0);
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector2(x, y).normalized * moveSpeed;
    }
}
