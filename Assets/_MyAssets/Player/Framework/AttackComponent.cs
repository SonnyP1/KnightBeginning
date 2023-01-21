using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] CameraShaker _cameraShaker;
    [SerializeField] BoxCollider _boxColliderRef;
    [SerializeField] LayerMask EnemyLayerMask;
    [SerializeField] float AttackCooldownTime = 0.2f;
    private bool isCooldownActive = false;
    Animator _animator;
    public void AddAttackMultiplier(float val) { _attackMultiplier = Mathf.Clamp(_attackMultiplier+val,0, float.MaxValue); }
    public float _attackMultiplier = 1.0f;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartAttack()
    {
        if(!isCooldownActive)
        {
            _animator.SetTrigger("AttackTrigger");
            StartCoroutine(AttackCooldown());
        }
    }
    public virtual void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(_boxColliderRef.gameObject.transform.position, _boxColliderRef.size / 2, Quaternion.identity, EnemyLayerMask);
        foreach (Collider col in colliders)
        {
            col.GetComponent<Enemy>().Death();
            _cameraShaker.ShakeCamera(0.5f, 0.1f);

            StartCoroutine(HitStun(.1f));
            GetComponent<MovementComponent>().ResetJump();
        }
    }

    IEnumerator AttackCooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(AttackCooldownTime * _attackMultiplier);
        isCooldownActive = false;
    }

    IEnumerator HitStun(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
