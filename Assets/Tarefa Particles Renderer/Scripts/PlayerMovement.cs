using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float vel = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] int jumpsAmount = 2;

    
    [HideInInspector] public int currentJumpsAmount;
    [HideInInspector] public bool isGrounded;

    [SerializeField] Rigidbody2D rb;

    float moveDirection;


    void OnEnable()
    {
        InputController.OnMoveInput += Move;
        InputController.OnJumpInput += Jump;
    }

    void OnDisable()
    {
        InputController.OnMoveInput -= Move;
        InputController.OnJumpInput -= Jump;
    }

    void Start()
    {
        ResetJumps();
    }

    void Move(float direction)
    {
        moveDirection = direction;
    }

    void Jump()
    {
        if (currentJumpsAmount <= 0)
            return;

        currentJumpsAmount -= 1;

        rb.linearVelocityY = 0;
        rb.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = moveDirection*vel;   
    }

    public void ResetJumps()
    {
        currentJumpsAmount = jumpsAmount;
    }

    public Vector2 GetCurrentVelocity()
    {
        return rb.linearVelocity;
    }
}
