using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] LayerMask PlayerMask;
    [SerializeField] BoxCollider _boxColliderRef;
    [SerializeField] GameObject BlowUpEffect;
    [SerializeField] float AttackDistance = 1f;

    [Header("Bool")]
    [SerializeField] bool hasTriggerBox;
    [SerializeField] bool isGroundedEnemy;
    [SerializeField] bool canBeKilled;

    public bool IsGroundedEnemy() {return isGroundedEnemy;}

    CameraShaker _cameraShaker;
    Animator _animator;
    GameObject player;
    private void Start()
    {
        _cameraShaker = FindObjectOfType<CameraShaker>();
        player = FindObjectOfType<PlayerController>().gameObject;
        _animator = GetComponent<Animator>();
        transform.parent = null;
    }
    public void Death()
    {
        if(canBeKilled)
        {
            UpdateScore();
            GameObject blowUpEffectObj = Instantiate(BlowUpEffect, this.transform);
            blowUpEffectObj.transform.parent = null;
            Destroy(gameObject);
        }
    }

    private void UpdateScore()
    {
        if(FindObjectOfType<ScoreSystem>() == null)
        {
            return;
        }
        FindObjectOfType<ScoreSystem>().AddScore(1.0f);
    }

    private void Update()
    {
        if(player== null)
        {
            return;
        }
        float distanceFromPlayer = Vector3.Distance(transform.position,player.transform.position);
        if(distanceFromPlayer < AttackDistance && _animator != null)
        {
            _animator.SetTrigger("AttackTrigger");
        }
    }

    public void AttackHitBox()
    {
        Collider[] colliders = Physics.OverlapBox(_boxColliderRef.gameObject.transform.position, _boxColliderRef.bounds.size/2, Quaternion.identity, PlayerMask);
        foreach (Collider col in colliders)
        {
            HealthComp playerHealth = col.gameObject.GetComponent<HealthComp>();
            if(playerHealth.GetCurrentHealth() > 0)
            {
                playerHealth.Hit(1,true);
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

    private void OnTriggerEnter(Collider other)
    {
        if(hasTriggerBox)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                HealthComp playerHealth = other.gameObject.GetComponent<HealthComp>();
                playerHealth.Hit(1,true);
                _cameraShaker.ShakeCamera(1f, 0.1f);
                if (playerHealth.GetCurrentHealth() > 0)
                {
                    StartCoroutine(HitStun(.2f));
                }
            }
        }
    }
}
