using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static event Action<float> OnMoveInput;
    public static event Action OnJumpInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue value)
    {
        Vector2 direction = value.Get<Vector2>();
        direction.Normalize();
        OnMoveInput?.Invoke(direction.x);
    }

    public void OnJump()
    {
        OnJumpInput?.Invoke();
    }
}
