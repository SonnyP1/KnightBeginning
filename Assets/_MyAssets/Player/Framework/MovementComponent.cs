using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float GroundSphereRadius = 0.1f;
    [SerializeField] CameraShaker _cameraShaker;
    [SerializeField] float MoveSpeed = 3f;
    [SerializeField] float SpeedModifier = 1f;
    [SerializeField] float JumpSpeed = 3f;
    [SerializeField] float HitPushBack = 3f;
    [SerializeField] GameObject JumpEffect;
    [SerializeField] Transform JumpSpawnLocEffect;


    bool isGrounded = true;
    int jumpCounter = 0;
    int maxJumpCount = 2;
    float fastFallMultiplier = 1f;
    bool isFastFalling = false;
    CharacterController _characterController;
    Animator _animator;
    float _gravity = -9.8f;
    Vector3 moveVelocity;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    public void AddToJumpCount(int val)
    {
        maxJumpCount = maxJumpCount + val;
    }
    public void Jump()
    {
        if (jumpCounter < maxJumpCount)
        {
            _cameraShaker.ShakeCamera(0.05f,0.1f);

            _animator.SetTrigger("DoubleJumpTrigger");
            moveVelocity.y = JumpSpeed;
            jumpCounter++;
        }
    }
    public void SpawnJumpEffect()
    {
        GameObject tempObj = Instantiate(JumpEffect, JumpSpawnLocEffect);
        tempObj.transform.position = JumpSpawnLocEffect.position;
        tempObj.transform.parent = null;
    }

    internal void Dash()
    {
        jumpCounter = 0;
        _animator.SetTrigger("DashTrigger");
        moveVelocity.x += 1 * HitPushBack;
    }

    void Update()
    {
        CheckIsGrounded();
        _animator.SetBool("isGrounded", isGrounded);
        if(_characterController.isGrounded)
        {
            jumpCounter = 0;
        }
        moveVelocity.y += (_gravity*fastFallMultiplier) * Time.deltaTime;
        _characterController.Move(moveVelocity*Time.deltaTime);
    }

    public void PushBackHit()
    {
        moveVelocity.x += -1 * HitPushBack;
    }

    public void StopDash()
    {
        moveVelocity.x = 0;
    }
    public void ResetMovement()
    {
        StopAllCoroutines();
        StartCoroutine(ResetingMovement());
    }
    IEnumerator ResetingMovement()
    {
        while(moveVelocity.x < 0)
        {
            moveVelocity.x += Time.deltaTime * (HitPushBack * 2);
            yield return new WaitForEndOfFrame();
        }
    }
    internal void FastFall()
    {
        if(!isFastFalling)
        {
            isFastFalling = true;
            fastFallMultiplier = 4.0f;            
        }
        else
        {
            isFastFalling = false;
            fastFallMultiplier = 1f;
        }
    }

    public void Move(float x)
    {
        StopAllCoroutines();
        moveVelocity.x = x * (MoveSpeed*SpeedModifier);
    }

    private void CheckIsGrounded()
    {
        Collider[] col = Physics.OverlapSphere(gameObject.transform.position, GroundSphereRadius, GroundLayer);
        if(col.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    internal void ResetJump()
    {
        jumpCounter = 0;
    }

    internal void ApplyMovementStat(float multiplier)
    {
        Debug.Log(multiplier);
        SpeedModifier += multiplier;
        Debug.Log("Added speed Multiplier");
    }
}
