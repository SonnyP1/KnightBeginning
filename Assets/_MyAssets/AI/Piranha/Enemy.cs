using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] LayerMask PlayerMask;
    [SerializeField] BoxCollider _boxColliderRef;
    [SerializeField] GameObject BlowUpEffect;
    [SerializeField] float AttackDistance = 1f;
    CameraShaker _cameraShaker;
    Animator _animator;
    GameObject player;
    private void Start()
    {
        _cameraShaker = FindObjectOfType<CameraShaker>();
        player = FindObjectOfType<PlayerController>().gameObject;
        _animator = GetComponent<Animator>();
    }
    public void Death()
    {
        GameObject blowUpEffectObj = Instantiate(BlowUpEffect, this.transform);
        blowUpEffectObj.transform.parent = null;
        Destroy(gameObject);
    }

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position,player.transform.position);
        if(distanceFromPlayer < AttackDistance)
        {
            _animator.SetTrigger("AttackTrigger");
        }
    }

    public void AttackHitBox()
    {
        Collider[] colliders = Physics.OverlapBox(_boxColliderRef.gameObject.transform.position, _boxColliderRef.size / 2, Quaternion.identity, PlayerMask);
        foreach (Collider col in colliders)
        {
            HealthComp playerHealth = col.gameObject.GetComponent<HealthComp>();
            if(playerHealth.GetCurrentHealth() > 0)
            {
                playerHealth.Hit();
                _cameraShaker.ShakeCamera(1f, 0.1f);
                if(playerHealth.GetCurrentHealth() > 0)
                {
                    StartCoroutine(HitStun(.2f));
                }
            }
        }
    }
    IEnumerator HitStun(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
