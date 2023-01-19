using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputsAction _playerInputs;
    MovementComponent _movementComp;
    Animator _animator;

    private void OnDisable()
    {
        _playerInputs.Disable();
    }

    public void DisableGameplayInputs()
    {
        _playerInputs.Gameplay.Disable();
    }
    public void EnabledGameplayInputs()
    {
        _playerInputs.Gameplay.Enable();
    }
    void Start()
    {
        _movementComp = GetComponent<MovementComponent>();
        _animator = GetComponent<Animator>();
        _playerInputs = new PlayerInputsAction();
        SetUpInputs();
    }

    void SetUpInputs()
    {
        _playerInputs.Enable();

        _playerInputs.Gameplay.Jump.performed += Jump;
        _playerInputs.Gameplay.Attack.performed += AttackPressed;

        _playerInputs.Gameplay.Move.performed += Move;
        _playerInputs.Gameplay.Move.canceled += Move;

        _playerInputs.Gameplay.FastFall.performed += FastFall;
        _playerInputs.Gameplay.FastFall.canceled += FastFall;

        _playerInputs.Gameplay.Dash.performed += Dash;
    }

    private void Dash(InputAction.CallbackContext obj)
    {
        _movementComp.Dash();
    }

    private void FastFall(InputAction.CallbackContext obj)
    {
        _movementComp.FastFall();
    }

    private void Move(InputAction.CallbackContext obj)
    {
        _movementComp.Move(obj.ReadValue<float>());
    }

    private void AttackPressed(InputAction.CallbackContext obj)
    {
        _animator.SetTrigger("AttackTrigger");
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        _movementComp.Jump();
    }
}
