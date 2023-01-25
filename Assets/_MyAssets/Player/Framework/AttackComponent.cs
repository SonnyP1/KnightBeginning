using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnKillCountChange();

public class AttackComponent : MonoBehaviour
{
    [SerializeField] CameraShaker _cameraShaker;
    [SerializeField] BoxCollider _boxColliderRef;
    [SerializeField] LayerMask EnemyLayerMask;
    [SerializeField] float AttackCooldownTime = 0.2f;
    [SerializeField] Image AttackCooldownImage;
    private int killCount = 0;
    private bool isCooldownActive = false;
    Animator _animator;

    public OnKillCountChange onKillCountChange;
    public void AddAttackMultiplier(float val) { _attackMultiplier = Mathf.Clamp(_attackMultiplier+val,0, float.MaxValue); }
    public float _attackMultiplier = 1.0f;
    void Start()
    {
        _animator = GetComponent<Animator>();
        AttackCooldownImage.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void UpdateKillCount()
    {
        killCount++;
        if(onKillCountChange != null)
        {
            onKillCountChange.Invoke();
        }
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
            if (col.GetComponent<Enemy>())
            {
                col.GetComponent<Enemy>().Death();
                UpdateKillCount();

            }
            else if(col.GetComponent<Boss>())
            {
                col.GetComponent<Boss>().Hit();
            }


            _cameraShaker.ShakeCamera(0.5f, 0.1f);
            StartCoroutine(HitStun(.1f));
            GetComponent<MovementComponent>().ResetJump();
        }
    }

    IEnumerator AttackCooldown()
    {
        isCooldownActive = true;
        float cooldownTime = AttackCooldownTime * _attackMultiplier;
        float time = 0.0f;
        while(time < cooldownTime)
        {
            time += Time.deltaTime;
            float percentage = time / cooldownTime;
            AttackCooldownImage.fillAmount = percentage;
            yield return new WaitForEndOfFrame();
        }


        AttackCooldownImage.fillAmount = 1;
        isCooldownActive = false;
    }

    IEnumerator HitStun(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
