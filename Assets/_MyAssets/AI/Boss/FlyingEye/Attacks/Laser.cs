using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] LayerMask _playerMask;
    [SerializeField] float _laserTimer;
    [SerializeField] SpriteRenderer _laserSpriteRender;
    [SerializeField] BoxCollider _boxColliderRef;

    private void Start()
    {
        StartCoroutine(StartLaserAttack());
    }

    IEnumerator StartLaserAttack()
    {

        yield return new WaitForSeconds(_laserTimer/4);
        _laserSpriteRender.color = new Color(255,255,255,100);    

        yield return new WaitForSeconds(_laserTimer/4);
        _laserSpriteRender.color = new Color(255,255,255,255);    
        

        yield return new WaitForSeconds(_laserTimer/4);
        _laserSpriteRender.color = new Color(255,255,255,100);    

        yield return new WaitForSeconds(_laserTimer/4);
        _laserSpriteRender.color = new Color(255,255,255,255);    

        //Do Overlap here
        SpawnHitBox();
    }

    void SpawnHitBox()
    {
        Collider[] colliders = Physics.OverlapBox(_boxColliderRef.gameObject.transform.position, _boxColliderRef.bounds.size / 2, Quaternion.identity, _playerMask);
        foreach (Collider col in colliders)
        {
            HealthComp playerHealth = col.gameObject.GetComponent<HealthComp>();
            if (playerHealth.GetCurrentHealth() > 0)
            {
                playerHealth.Hit(1, true);
                FindObjectOfType<CameraShaker>().ShakeCamera(1f, 0.1f);
                if (playerHealth.GetCurrentHealth() > 0)
                {
                    StartCoroutine(HitStun(.2f));
                }
            }
        }
        Destroy(gameObject);
    }

    IEnumerator HitStun(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
