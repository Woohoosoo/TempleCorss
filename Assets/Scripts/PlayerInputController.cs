using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownController
{
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnMove(InputValue Value)
    {
        Vector2 moveInput = Value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);

    }
}
