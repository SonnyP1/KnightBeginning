using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.PostProcessing;

public class HealthComp : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera deathCam;
    [SerializeField] float CurrentHealth;
    [SerializeField] float MaxHealth;
    [SerializeField] PostProcessVolume _postProcessingVolume;
    private ChromaticAberration _cA;
    Animator _animator;
    MovementComponent _movementComponent;
    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movementComponent = GetComponent<MovementComponent>();
        CurrentHealth = MaxHealth;
    }
    public void Hit()
    {
        _animator.SetTrigger("HitTrigger");
        CurrentHealth = Mathf.Clamp(CurrentHealth-1 ,0,MaxHealth);
        _movementComponent.PushBackHit();
        StartCoroutine(ApplyChromaticAberration(.2f,1f));
        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        StartCoroutine(DeathStun(5f));
        GetComponent<PlayerController>().enabled = false;
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in allEnemies)
        {
            enemy.enabled = false;
        }

        Destroy(FindObjectOfType<Generator>().gameObject);
        Track[] allTracker = FindObjectsOfType<Track>();
        foreach(Track track in allTracker)
        {
            track.SetTrackMovementSpeed(0.0f);
        }

        deathCam.Priority = 100;
        _animator.SetTrigger("DeathTrigger");
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
