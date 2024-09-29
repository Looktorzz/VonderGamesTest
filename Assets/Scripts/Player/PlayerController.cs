using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> WhenDirectionMoveChanged;
    public event Action WhenJumped;
    public event Action WhenInteracted;

    private InputSystem _inputSystem;
    private bool _isInit;

    public void SetUp()
    {
        Init();

        _inputSystem.Enable();

        _inputSystem.Player.Move.performed += OnMovementPerformed;
        _inputSystem.Player.Move.canceled += OnMovementCanceled;

        _inputSystem.Player.Jump.started += OnJumpStarted;
        _inputSystem.Player.Interact.started += OnInteractStarted;
    }

    public void UnsubscribeInput()
    {
        _inputSystem.Player.Move.performed -= OnMovementPerformed;
        _inputSystem.Player.Move.canceled -= OnMovementCanceled;

        _inputSystem.Player.Jump.started -= OnJumpStarted;
        _inputSystem.Player.Interact.started -= OnInteractStarted;

        _inputSystem.Disable();
    }

    private void Init()
    {
        if (_isInit)
        {
            return;
        }

        _inputSystem = new InputSystem();
        _isInit = true;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        float directionX = value.ReadValue<Vector2>().x;
        Vector2 directionMove = new Vector2(directionX, 0);
        WhenDirectionMoveChanged?.Invoke(directionMove);
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        Vector2 directionMove = Vector2.zero;
        WhenDirectionMoveChanged?.Invoke(directionMove);
    }

    private void OnJumpStarted(InputAction.CallbackContext value)
    {
        if (value.ReadValueAsButton())
        {
            WhenJumped?.Invoke();
        }
    }

    private void OnInteractStarted(InputAction.CallbackContext value)
    {
        if (value.ReadValueAsButton())
        {
            WhenInteracted?.Invoke();
        }
    }
}
