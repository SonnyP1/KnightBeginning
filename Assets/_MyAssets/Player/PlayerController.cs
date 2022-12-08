using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CameraShaker _cameraShaker;
    [SerializeField] BoxCollider _boxColliderRef;
    [SerializeField] LayerMask EnemyLayerMask;
    PlayerInputsAction _playerInputs;
    MovementComponent _movementComp;
    Animator _animator;

    private void OnDisable()
    {
        _playerInputs.Disable();
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
        _playerInputs.Gameplay.Attack.performed += Attack;

        _playerInputs.Gameplay.Move.performed += Move;
        _playerInputs.Gameplay.Move.canceled += Move;

        _playerInputs.Gameplay.FastFall.performed += FastFall;
        _playerInputs.Gameplay.FastFall.canceled += FastFall;
    }

    private void FastFall(InputAction.CallbackContext obj)
    {
        _movementComp.FastFall();
    }

    private void Move(InputAction.CallbackContext obj)
    {
        _movementComp.Move(obj.ReadValue<float>());
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        Debug.Log("ATTCK NOW");
        _animator.SetTrigger("AttackTrigger");
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        _movementComp.Jump();
    }

    public void AttackHitBox()
    {
        Collider[] colliders = Physics.OverlapBox(_boxColliderRef.gameObject.transform.position,_boxColliderRef.size/2,Quaternion.identity,EnemyLayerMask);
        foreach(Collider col in colliders)
        {
            col.GetComponent<Enemy>().Death();
            _cameraShaker.ShakeCamera(0.5f,0.1f);

            StartCoroutine(HitStun(.1f));
            _movementComp.ResetJump();
        }
    }

    IEnumerator HitStun(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
