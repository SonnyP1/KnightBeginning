using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.PostProcessing;
using System;

public delegate void OnDmgTaken(int val);
public delegate void OnDeath();

public class HealthComp : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera deathCam;
    [SerializeField] float CurrentHealth;
    [SerializeField] float MaxHealth;
    float dmgMultiplier = 1.0f;
    internal void SetDmgMultiplier(float newVal)
    {
        dmgMultiplier = newVal;
    }

    [SerializeField] PostProcessVolume _postProcessingVolume;
    public OnDeath onDeath;
    public OnDmgTaken onDmgTaken;
    private InGameUISystem _inGameUISystem;
    private ChromaticAberration _cA;
    Animator _animator;
    MovementComponent _movementComponent;
    internal bool canTakeDmg = true;
    private bool isDead = false;

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movementComponent = GetComponent<MovementComponent>();
        _inGameUISystem = FindObjectOfType<InGameUISystem>();

        CurrentHealth = MaxHealth;
        _inGameUISystem.UpdateHealthUI((int)CurrentHealth);
    }
    public void Hit(int dmg, bool isDeathBlow = true)
    {
        if(canTakeDmg)
        {
            _animator.SetTrigger("HitTrigger");
            if(onDmgTaken != null)
            {
                onDmgTaken.Invoke(dmg);
            }
            CurrentHealth = Mathf.Clamp(CurrentHealth - (dmg*dmgMultiplier) ,0, CurrentHealth);
            dmgMultiplier = 1.0f;

            _movementComponent.PushBackHit();
            StartCoroutine(ApplyChromaticAberration(.2f,1f));
            if (CurrentHealth <= 0 && isDeathBlow)
            {
                if(!isDead)
                {
                    Death();
                }
            }
            _inGameUISystem.UpdateHealthUI((int)CurrentHealth);
        }
    }

    public void Heal(int heal)
    {
        CurrentHealth += heal;
        _inGameUISystem.UpdateHealthUI((int)CurrentHealth);
    }
    void Death()
    {
        isDead = true;
        onDeath.Invoke();
        StartCoroutine(DeathStun(5f));
        GetComponent<PlayerController>().enabled = false;
        SimpleMove[] allMovementObj = FindObjectsOfType<SimpleMove>();
        foreach(SimpleMove movementObj in allMovementObj)
        {
            movementObj.StopMovement();
            Enemy movementObjAsEnemy = movementObj.GetComponent<Enemy>();
            if(movementObjAsEnemy != null)
            {
                movementObjAsEnemy.enabled = false;
            }
        }
        Destroy(FindObjectOfType<EnemySpawner>().gameObject);

        Destroy(FindObjectOfType<Generator>().gameObject);
        Track[] allTracker = FindObjectsOfType<Track>();
        foreach(Track track in allTracker)
        {
            track.SetTrackMovementSpeed(0.0f);
        }


        SetDeathCamPriority(100);
        _animator.SetTrigger("DeathTrigger");
    }

    public void SetDeathCamPriority(int val)
    {
        deathCam.Priority = val;
    }
    IEnumerator DeathStun(float duration)
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    IEnumerator ApplyChromaticAberration(float value, float duration)
    {
        _postProcessingVolume.profile.TryGetSettings(out _cA);
        _cA.intensity.value += value;
        yield return new WaitForSecondsRealtime(duration);
        _cA.intensity.value -= value;
    }
}
