using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class GroundCheck : MonoBehaviour
{
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckBoxSize;
    [SerializeField] LayerMask groundLayerMask;

    PlayerMovement playerMovement;

    bool isGrounded;


    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        playerMovement.isGrounded = isGrounded;

        if (isGrounded)
        {
            playerMovement.ResetJumps();
        }

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckBoxSize, 0f, groundLayerMask);
    }
}
